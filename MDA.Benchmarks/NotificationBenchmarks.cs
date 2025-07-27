using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using MDA.Enum;
using MDA.Implementation;
using MDA.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace MDA.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Throughput)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class NotificationBenchmarks
    {
        private readonly IBClient _client;
        private readonly List<(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)> _notificationModel_1000;
        //private readonly List<IBNotificationStruct> _notificationStruct_1000;

        public NotificationBenchmarks()
        {
            _client = new IBClient(new ConfigurationBuilder().Build());
            _notificationModel_1000 = Enumerable.Range(0, 1_000)
                               .Select(i => (id: -1, errorCode: 2104, errorMsg: "Market data farm connection is OK:hfarm", advancedOrderRejectJson: ""))
                               .ToList();
            _client.NotificationReceived += (_) => { };
        }

        [Benchmark(Baseline = true)]
        public void NotificationBase()
        {
            foreach (var message in _notificationModel_1000)
                _client.error(message.id, message.errorCode, message.errorMsg, message.advancedOrderRejectJson);
        }
    }
}
