using BenchmarkDotNet.Running;

namespace MDA.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<NotificationBenchmarks>();
        }
    }
}
