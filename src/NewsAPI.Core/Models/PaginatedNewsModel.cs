using NewsAPI.Core.Entities;

namespace NewsAPI.Core.Models;

public class PaginatedNewsModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public List<NewsEntity> News { get; set; } = new List<NewsEntity>();
}
