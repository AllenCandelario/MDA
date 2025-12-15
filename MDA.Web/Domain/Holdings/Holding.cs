using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Instruments;
using MDA.Web.Domain.Categories;
using System.Diagnostics;

namespace MDA.Web.Domain.Holdings
{
    public sealed class Holding
    {
        public Guid Id { get; private set; }

        public Guid AccountId { get; private set; }
        public Guid InstrumentId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public string? Notes { get; private set; }
        public int? Rating { get; private set; }

        // Frequently Updated data
        public decimal Quantity { get; private set; }
        public decimal AveragePrice { get; private set; }
        public decimal UnrealizedPNL { get; private set; }
        public decimal RealizedPNL { get; private set; }

        // Navigation
        public Account Account { get; private set; } = null!;
        public Instrument Instrument { get; private set; } = null!;
        public Category? Category { get; private set; } = null!;


        // Constructors
        private Holding() { }

        public Holding(Guid accountId, Guid instrumentId, Guid? categoryId, decimal quantity, decimal averagePrice)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            InstrumentId = instrumentId;
            CategoryId = categoryId;
            Quantity = quantity;
            AveragePrice = averagePrice;
        }


        // Domain methods
        public void UpdatePosition(decimal quantity, decimal averagePrice, decimal unrealizedPNL, decimal realizedPNL)
        {
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (averagePrice < 0) throw new ArgumentOutOfRangeException(nameof(averagePrice));

            Quantity = quantity;
            AveragePrice = averagePrice;
            UnrealizedPNL = unrealizedPNL;
            RealizedPNL = realizedPNL;
        }

        public void AssignCategory(Guid? categoryId)
        {
            CategoryId = categoryId;
        }

        public void UpdateNotes(string? notes) => Notes = notes;
        public void UpdateRating(int? rating) => Rating = rating;
    }
}
