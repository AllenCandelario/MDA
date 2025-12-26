namespace MDA.Web.API.DTOs
{
    public sealed record CategoryDto(Guid Id, string Name, string? Description);
    public sealed record CategoryGoalsDto(decimal? ShortTermPct, decimal? MidTermPct, decimal? LongTermPct);
    public sealed record CategorySummaryDto(string Category, decimal TotalMarketValue, decimal PctOfInvested, CategoryGoalsDto? Goals);
    public sealed record CreateCategoryRequest(string Name, string? Description);
    public sealed record UpdateCategoryRequest(string Name, string? Description);
}
