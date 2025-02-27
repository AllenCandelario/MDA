using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace MDA.Implementation
{
    // Base EWrapper implementation for connection. Other method implementations will be split into other classes, so we're declaring this class with the partial keyword
    public partial class IBClient : EWrapper
    {
        internal readonly EClientSocket _clientSocket;
        internal readonly EReaderSignal _signal;
        
        internal readonly string _host;
        internal readonly int _port;
        internal readonly int _clientId;

        public IBClient(IConfiguration configuration)
        {
            _signal = new EReaderMonitorSignal();
            _clientSocket = new EClientSocket(this, _signal);

            // Default to standard localhost connection if environment variables are not available
            _host = configuration.GetSection("IBKRConfig:Host").Value ?? "127.0.0.1";
            if (!int.TryParse(configuration.GetSection("IBKRConfig:Port").Value, out _port))
            {
                _port = 4001;
            }

            if (!int.TryParse(configuration.GetSection("IBKRConfig:ClientId").Value, out _clientId))
            {
                _clientId = 0;
            }
        }

        public void InitiateConnection()
        {
            // Maybe retrieve the _configuration stuff here instad of hardcoding
            _clientSocket.eConnect(_host, _port, _clientId);

            //Create a reader to consume messages from the TWS. The EReader will consume the incoming messages and put them in a queue
            var reader = new EReader(_clientSocket, _signal);
            reader.Start();
            
            //Once the messages are in the queue, an additional thread can be created to fetch them
            new Thread(() => 
            { 
                while (_clientSocket.IsConnected()) 
                { 
                    _signal.waitForSignal(); 
                    reader.processMsgs(); 
                } 
            }) 
            { IsBackground = true }.Start();
        }

        public void error(Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        public void managedAccounts(string accountsList)
        {
            Console.WriteLine($"accountsList: {accountsList}");
        }

        public void nextValidId(int orderId)
        {
            Console.WriteLine($"orderId: {orderId}");
        }

        public void error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            Console.WriteLine($"IBKR error:\nid: {id}\nerrorCode: {errorCode}\nerrorMsg: {errorMsg}\nadvancedOrderRejectJson: {advancedOrderRejectJson}");
        }
    }
}
