using NewsAPI.Core.Entities;
using NewsAPI.Core.Helpers;
using NewsAPI.Core.Models;
using NewsAPI.SharedKernel;

namespace NewsAPI.Core.Interfaces;

public interface INewsService
{
    PaginatedList<NewsEntity> GetAll(int? page);
    Task<ModelOrError<NewsEntity>> AddAsync(NewsModel model);
    Task<ModelOrError<NewsEntity>> UpdateAsync(NewsModel model);
    Task<ModelOrError<NewsEntity>> DeleteAsync(int id);
}
