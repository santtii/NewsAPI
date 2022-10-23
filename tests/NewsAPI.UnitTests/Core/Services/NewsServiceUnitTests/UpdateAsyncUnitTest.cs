using AutoFixture.NUnit3;
using Moq;
using NewsAPI.Core.Entities;
using NewsAPI.Core.Interfaces.Data;
using NewsAPI.Core.Models;
using NewsAPI.Core.Services;
using NUnit.Framework;

namespace NewsAPI.UnitTests.Core.Services.NewsServiceUnitTests;

[TestFixture]
public class UpdateAsyncUnitTest
{
    [Theory, AutoDomainData]
    public async Task Verify_Updated_News_DetailsAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, NewsModel model)
    {
        // Arrange
        mockRepository.Setup(x => x.UpdateAsync(It.IsAny<NewsEntity>(), model.NewsId, It.IsAny<string>())).ReturnsAsync((NewsEntity)model);
        // Act
        var result = await sut.UpdateAsync(model);
        // Assert
        Assert.AreEqual(model.NewsId, result.Model?.Id);
        Assert.AreEqual(model.CategoryId, result.Model?.CategoryId);
        Assert.AreEqual(model.Image, result.Model?.Image);
        Assert.AreEqual(model.Title, result.Model?.Title);
        Assert.AreEqual(model.Body, result.Model?.Body);
        Assert.AreEqual(model.Date, result.Model?.Date);
        Assert.AreEqual(model.Visible, result.Model?.Visible);
    }

    [Theory, AutoDomainData]
    public async Task Verify_That_Changes_Are_SavedAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, NewsModel model)
    {
        // Act
        var result = await sut.UpdateAsync(model);
        // Assert
        mockRepository.Verify(x => x.UpdateAsync(It.IsAny<NewsEntity>(), model.NewsId, It.IsAny<string>()), Times.Once);
        mockRepository.Verify(x => x.SaveChangesAsync(true), Times.Once);
    }
}
