using MDA.Web.Domain.Categories;
using MDA.Web.Domain.Holdings;

namespace MDA.Web.Domain.Accounts
{
    public sealed class Account
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string IbkrAccountId { get; private set; }
        public string BaseCurrency { get; private set; } = "USD";
        public string AccountType { get; private set; }

        // Frequently updated data, nullable if details cannot be retrieved
        public DateTime? LastUpdatedUtc { get; set; }
        public decimal? TotalPortfolioValue { get; set; }
        public decimal? SettledCash {  get; set; }
        public decimal? ExcessLiquidity { get; set; }
        public decimal? BuyingPower { get; set; }
        public decimal? DailyPnl { get; set; } // Not part of AccountUpdate
        public decimal? UnrealizedPnl { get; set; }


        // TODO: Check if the below region should be placed on the application side / dividend domain

        #region Check these values
        public decimal? ExpectedDividendsYear { get; set; }
        public decimal? DividendsPaidYtd { get; set; }

        #endregion

        // Navigation
        public ICollection<Holding> Holdings { get; private set; } = new List<Holding>();
        public ICollection<Category> Categories { get; private set; } = new List<Category>();


        // Method properties
        public decimal? DividendsRemaining => ExpectedDividendsYear - DividendsPaidYtd;


        // Constructors
        private Account() { }
        
        public Account(Guid userId, string ibkrAccountId, string baseCurrency)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            IbkrAccountId = ibkrAccountId;
            BaseCurrency = baseCurrency;
            LastUpdatedUtc = DateTime.UtcNow;
        }
    }
}
