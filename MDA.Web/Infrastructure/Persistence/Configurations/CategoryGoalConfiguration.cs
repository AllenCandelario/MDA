using MDA.Web.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MDA.Web.Infrastructure.Persistence.Configurations
{
    public sealed class CategoryGoalConfiguration : IEntityTypeConfiguration<CategoryGoal>
    {
        public void Configure(EntityTypeBuilder<CategoryGoal> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.Category)
                .WithMany(c => c.Goals)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(g => g.Horizon)
                .HasConversion<string>()
                .HasMaxLength(32);

            builder.Property(g => g.TargetWeightPercent)
                .HasColumnType("numeric(5,2)");
        }
    }
}
