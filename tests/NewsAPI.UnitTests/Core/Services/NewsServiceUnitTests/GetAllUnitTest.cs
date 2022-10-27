using AutoFixture.NUnit3;
using Moq;
using NewsAPI.Core.Entities;
using NewsAPI.Core.Interfaces.Data;
using NewsAPI.Core.Services;
using NUnit.Framework;

namespace NewsAPI.UnitTests.Core.Services.NewsServiceUnitTests;

[TestFixture]
public class GetAllUnitTest
{
    private readonly List<NewsEntity> _news;

    public GetAllUnitTest()
    {
        _news = new List<NewsEntity>
        {
            new NewsEntity
            {
                Id = 1,
                CategoryId = 1,
                Image = "test-image",
                Title = "test-title",
                Body = "test-body",
                Date = DateTime.UtcNow,
                Visible = true
            },
            new NewsEntity
            {
                Id = 2,
                CategoryId = 3,
                Image = "test-image-2",
                Title = "test-title-2",
                Body = "test-body-2",
                Date = DateTime.UtcNow,
                Visible = false
            }
        };
    }

    [Theory, AutoDomainData]
    public async Task Verify_Returned_NewsAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut)
    {
        // Arrange
        mockRepository.Setup(x => x.GetAll<NewsEntity>()).Returns(_news.AsQueryable());
        // Act
        var result = await sut.GetAllAsync();
        // Assert
        Assert.That(_news, Has.All.Matches<NewsEntity>(e => result.News.Any(r => e.Id == r.Id && e.CategoryId == r.CategoryId
            && e.Image == r.Image && e.Title == r.Title && e.Body == r.Body && e.Date == r.Date && e.Visible == r.Visible)));
    }
}
