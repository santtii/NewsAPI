using NewsAPI.Core.Entities;
using NewsAPI.Core.Helpers;
using NewsAPI.Core.Interfaces;
using NewsAPI.Core.Interfaces.Data;
using NewsAPI.Core.Models;
using NewsAPI.SharedKernel;

namespace NewsAPI.Core.Services;

public class NewsService : INewsService
{
    private readonly IRepository _repository;

    public NewsService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<NewsEntity> GetAsync(int id)
    {
        return await _repository.FirstAsync<NewsEntity>(x => x.Id == id);
    }

    public PaginatedList<NewsEntity> GetAll(int? page)
    {
        return new PaginatedList<NewsEntity>(_repository.GetAll<NewsEntity>(), page ?? 0, 5);
    }

    public async Task<ModelOrError<NewsEntity>> AddAsync(NewsModel model)
    {
        var result = new ModelOrError<NewsEntity>();

        var category = await _repository.FirstAsync<CategoryEntity>(x => x.Id == model.CategoryId);
        if (category != null)
        {
            result.Model = await _repository.AddAsync((NewsEntity)model);
            await _repository.SaveChangesAsync();
        }
        else
        {
            result.AddError(ErrorCode.CATEGORY_NOT_FOUND);
        }
        return result;
    }

    public async Task<ModelOrError<NewsEntity>> UpdateAsync(NewsModel model)
    {
        var result = new ModelOrError<NewsEntity>
        {
            Model = await _repository.UpdateAsync((NewsEntity)model, model.NewsId)
        };
        await _repository.SaveChangesAsync();
        return result;
    }

    public async Task<ModelOrError<NewsEntity>> DeleteAsync(int id)
    {
        var result = new ModelOrError<NewsEntity>();

        _repository.Delete<NewsEntity>(id);
        await _repository.SaveChangesAsync();
        return result;
    }
}
