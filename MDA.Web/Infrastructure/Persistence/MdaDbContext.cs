using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Categories;
using MDA.Web.Domain.Holdings;
using MDA.Web.Domain.Instruments;
using Microsoft.EntityFrameworkCore;

namespace MDA.Web.Infrastructure.Persistence
{
    public sealed class MdaDbContext : DbContext
    {
        public MdaDbContext(DbContextOptions<MdaDbContext> options) : base(options) { }

        // TODO: Find out why in your GIC project, the DbSet line uses get; set; but here, gpt mentioned to use Set<T>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Instrument> Instruments => Set<Instrument>();
        public DbSet<Holding> Holdings => Set<Holding>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<CategoryGoal> CategoryGoals => Set<CategoryGoal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MdaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }



    }
}
