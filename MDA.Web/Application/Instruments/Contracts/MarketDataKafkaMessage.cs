namespace MDA.Web.Application.Instruments.Contracts
{
    public sealed class MarketDataKafkaMessage
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
