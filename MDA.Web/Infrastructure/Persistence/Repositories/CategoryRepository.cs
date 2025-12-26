using MDA.Web.Application.Categories.Interfaces;
using MDA.Web.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace MDA.Web.Infrastructure.Persistence.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly MdaDbContext _db;
        public CategoryRepository(MdaDbContext dbContext) 
        { 
            _db = dbContext;
        }

        public async Task<IReadOnlyList<Category>> GetAllCategoriesByAccountId(Guid accountId, CancellationToken ct)
        {
            return await _db.Categories
                .Include(c => c.Holdings)
                .Include(c => c.Goals)
                .Where(c => c.AccountId == accountId)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Category category, CancellationToken ct)
        {
            await _db.AddAsync(category, ct);
        }
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
