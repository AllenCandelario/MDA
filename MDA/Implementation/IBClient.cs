using IBApi;
using MDA.Enum;
using MDA.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MDA.Implementation
{
    // Base EWrapper implementation for connection. Other method implementations will be split into an extension of this class, so we're declaring this class with the partial keyword
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

            #region Default to standard localhost connection if environment variables are not available
            _host = configuration.GetSection("IBKRConfig:Host").Value ?? "127.0.0.1";
            
            if (!int.TryParse(configuration.GetSection("IBKRConfig:Port").Value, out _port))
            {
                _port = 4001;
            }

            if (!int.TryParse(configuration.GetSection("IBKRConfig:ClientId").Value, out _clientId))
            {
                _clientId = 0;
            }
            #endregion
        }

        public void InitiateConnection()
        {
            _clientSocket.eConnect(_host, _port, _clientId);

            // Create a reader to consume messages from the TWS. The EReader will consume the incoming messages and put them in a queue
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

            // Consider using the Task way but test out the perfomance first
            //_ = Task.Run(async () =>
            //{
            //    while (_clientSocket.IsConnected())
            //    {
            //        _signal.waitForSignal();   // still blocks; can wrap in TaskCompletionSource
            //        reader.processMsgs();
            //        await Task.Yield();        // cooperative
            //    }
            //});
        }

        void EWrapper.connectAck()
        {
            Console.WriteLine("Connection Acknowledged");
        }

        void EWrapper.connectionClosed()
        {
            Console.WriteLine("Connection Closed");
        }

        // To move to another class 
        void EWrapper.managedAccounts(string accountsList)
        {
            Console.WriteLine($"accountsList: {accountsList}");
        }

        // To move to another partial class
        void EWrapper.nextValidId(int orderId)
        {
            Console.WriteLine($"orderId: {orderId}");
        }
    }
}
