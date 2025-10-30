using IBApi;
using MDA.Config;
using MDA.Enum;
using MDA.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MDA.Implementation
{
/*
    DECISIONS:
    - God class IBClient (with the partial keyword) since EWrapper contains all the methods. This main class handles connection-related methods 
    - Calls one dedicated background thread (new Thread keyword) to handle message processing

    FUTURE IMPROVEMENTS:
    - Using a dedicated thread or core via the ProcessThread.ProcessorAffinity or IdealProcessor keywords
    - Using a thread-pool task via the Task keyword 
*/
    public partial class IBClient : EWrapper, IDisposable
    {
        internal readonly EClientSocket _clientSocket;
        internal readonly EReaderSignal _signal;
        internal Thread _readerThread;
        
        private readonly IBKRConfigOptions _ibkrConfig;

        public IBClient(IOptions<IBKRConfigOptions> options)
        {
            _ibkrConfig = options.Value;
            _signal = new EReaderMonitorSignal();
            _clientSocket = new EClientSocket(this, _signal);
        }

        public void InitiateConnection()
        {
            _clientSocket.eConnect(_ibkrConfig.Host, _ibkrConfig.Port, _ibkrConfig.ClientId);

            // Create a reader to consume messages from the TWS. The EReader will consume the incoming messages and put them in a queue
            var reader = new EReader(_clientSocket, _signal);
            reader.Start();

            //Once the messages are in the queue, an additional thread can be created to fetch them
            _readerThread = new Thread(() =>
            {
                while (_clientSocket.IsConnected())
                {
                    _signal.waitForSignal();
                    reader.processMsgs();
                }
            })
            { IsBackground = true };
            _readerThread.Start();
        }

        public void Dispose()
        {
            _clientSocket.eDisconnect(); // closes TCP socket
            _readerThread.Join(); // Blocks calling thread until the while(_clientSocket.Isconnect()) exits
        }

        void EWrapper.connectAck()
        {
            Console.WriteLine("Connection Acknowledged");
        }

        void EWrapper.connectionClosed()
        {
            Console.WriteLine("Connection Closed");
        }

        // To move to another partial class
        void EWrapper.nextValidId(int orderId)
        {
            Console.WriteLine($"orderId: {orderId}");
        }
    }
}
