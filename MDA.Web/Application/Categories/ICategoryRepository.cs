using MDA.Web.Domain.Accounts;
using MDA.Web.Domain.Categories;

namespace MDA.Web.Application.Categories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllCategoriesByAccountId(Guid accountId, CancellationToken ct);
        Task AddAsync(Category category, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
