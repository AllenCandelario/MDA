using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using MDA.Implementation;
using MDA.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Abstractions;

namespace MDA.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Throughput)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class NotificationBenchmarks
    {
        private IBClient _client;
        private List<(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)> _msgs;

        [GlobalSetup]
        public void Setup()
        {
            var opts = Options.Create(new IBKRConfigOptions { Host = "127.0.0.1", Port = 4001, ClientId = 0 });
            var logger = NullLogger<IBClient>.Instance; // no op logger

            _client = new IBClient(opts, logger);
            _client.NotificationReceived += _ => { /* no-op */ };

            _msgs = Enumerable.Range(0, 1_000)
                .Select(i => (-1, 2104, "Market data farm connection is OK:hfarm", ""))
                .ToList();
        }

        [Benchmark(Baseline = true)]
        public void NotificationBase()
        {
            foreach (var m in _msgs)
                _client.error(m.id, m.errorCode, m.errorMsg, m.advancedOrderRejectJson);
        }
    }
}
