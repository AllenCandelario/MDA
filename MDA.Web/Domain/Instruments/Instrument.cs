using MDA.Web.Domain.Holdings;

namespace MDA.Web.Domain.Instruments
{
    public sealed class Instrument
    {
        public Guid Id { get; private set; }
        public string Symbol { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public int IbkrConId { get; private set; } // TODO: Not sure what this is for, double check 
        public string AssetClass { get; private set; } = "Stock"; // TODO: Enum / const?

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

        public Instrument(string symbol, string name, int ibkrConId, string assetClass = "Stock")
        {
            Id = Guid.NewGuid();
            Symbol = symbol;
            Name = name;
            IbkrConId = ibkrConId;
            AssetClass = assetClass;
        }

        public void UpdateMarketData(decimal lastPrice, DateTime updatedUtc, decimal? pe = null, decimal? forwardPe = null, decimal? week52High = null, decimal? ath = null)
        {
            LastPrice = lastPrice;
            LastPriceUpdatedUtc = DateTime.UtcNow;
            Pe = pe ?? Pe;
            ForwardPe = forwardPe ?? ForwardPe;
            Week52High = week52High ?? Week52High;
            Ath = ath ?? Ath;
        }
    }
}
