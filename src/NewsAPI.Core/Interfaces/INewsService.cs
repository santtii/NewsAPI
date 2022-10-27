using NewsAPI.Core.Entities;
using NewsAPI.Core.Models;
using NewsAPI.SharedKernel;

namespace NewsAPI.Core.Interfaces;

public interface INewsService
{
    Task<PaginatedNewsModel> GetAllAsync(int? page = null);
    Task<ModelOrError<NewsEntity>> AddAsync(NewsModel model);
    Task<ModelOrError<NewsEntity>> UpdateAsync(NewsModel model);
    Task<ModelOrError<NewsEntity>> DeleteAsync(int id);
}
