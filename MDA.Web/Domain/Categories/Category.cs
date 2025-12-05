using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Holdings;

namespace MDA.Web.Domain.Categories
{
    public sealed class Category
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        // Navigation
        public Account Account { get; private set; } = null!;
        public ICollection<Holding> Holdings { get; private set; } = new List<Holding>();
        public ICollection<CategoryGoal> Goals { get; private set; } = new List<CategoryGoal>();

        private Category() { }

        public Category(Guid accountId, string name, string? description = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            AccountId = accountId;
            Description = description;
        }
    }
}
