using MDA.Web.Domain.Holdings;

namespace MDA.Web.Domain.Instruments
{
    public sealed class Instrument
    {
        public Guid Id { get; private set; }
        public string Symbol { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public int IbkrConId { get; private set; }
        public string AssetClass { get; private set; }

        // Frequently updated data, nullable if details cannot be retrieved
        public decimal? LastPrice { get; private set; }
        public DateTime? LastPriceUpdatedUtc { get; private set; }
        public decimal? Pe { get; private set; }
        public decimal? ForwardPe { get; private set; }
        public decimal? Week52High { get; private set; }
        public decimal? Ath { get; private set; }

        // Navigation
        public ICollection<Holding> Holdings { get; private set; } = new List<Holding>(); // TODO: Why is this needed?

        
        // Constructors
        public Instrument() { } // EF

        public Instrument(string symbol, string name, int ibkrConId, string assetClass)
        {
            Id = Guid.NewGuid();
            Symbol = symbol;
            Name = name;
            IbkrConId = ibkrConId;
            AssetClass = assetClass;
        }

        public void Update52WeekHigh(decimal week52High)
        {
            Week52High = week52High;
        }

        public void UpdateLastPrice(decimal lastPrice)
        {
            LastPrice = lastPrice;
            LastPriceUpdatedUtc = DateTime.UtcNow;
        }

        // PEs may be nullable
        public void UpdatePeRatios(decimal? pe, decimal? forwardPe)
        {
            if (pe.HasValue) Pe = pe.Value;
            if (forwardPe.HasValue) ForwardPe = forwardPe.Value;
        }

        public void UpdateATH(decimal ath)
        {
            Ath = ath;
        }
    }
}
