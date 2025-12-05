namespace MDA.Web.Domain.Categories
{
    public sealed class CategoryGoal
    {
        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; }
        public GoalHorizon Horizon { get; private set; }
        public decimal TargetWeightPercent { get; private set; }

        // Navigation
        public Category Category { get; private set; } = null!;

        private CategoryGoal() { }

        public CategoryGoal(Guid categoryId, GoalHorizon horizon, decimal targetWeightPercent)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Horizon = horizon;
            TargetWeightPercent = targetWeightPercent;
        }
    }
}
