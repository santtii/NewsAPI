using AutoFixture.NUnit3;
using Moq;
using NewsAPI.Core.Entities;
using NewsAPI.Core.Interfaces.Data;
using NewsAPI.Core.Services;
using NUnit.Framework;

namespace NewsAPI.UnitTests.Core.Services.NewsServiceUnitTests;

[TestFixture]
public class GetAsyncUnitTest
{
    private readonly NewsEntity _news;

    public GetAsyncUnitTest()
    {
        _news = new NewsEntity
        {
            Id = 1,
            CategoryId = 1,
            Image = "test-image",
            Title = "test-title",
            Body = "test-body",
            Date = DateTime.UtcNow,
            Visible = true
        };
    }

    [Theory, AutoDomainData]
    public async Task Verify_Returned_NewsAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, int id)
    {
        // Arrange
        mockRepository.Setup(x => x.FirstAsync<NewsEntity>(x => x.Id == id)).ReturnsAsync(_news);
        // Act
        var result = await sut.GetAsync(id);
        // Assert
        Assert.AreEqual(_news.Id, result?.Id);
        Assert.AreEqual(_news.CategoryId, result?.CategoryId);
        Assert.AreEqual(_news.Image, result?.Image);
        Assert.AreEqual(_news.Title, result?.Title);
        Assert.AreEqual(_news.Body, result?.Body);
        Assert.AreEqual(_news.Date, result?.Date);
        Assert.AreEqual(_news.Visible, result?.Visible);
    }
}
