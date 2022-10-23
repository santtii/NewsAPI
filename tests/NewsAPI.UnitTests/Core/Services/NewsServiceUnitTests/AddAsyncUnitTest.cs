using AutoFixture.NUnit3;
using Moq;
using NewsAPI.Core.Entities;
using NewsAPI.Core.Interfaces.Data;
using NewsAPI.Core.Models;
using NewsAPI.Core.Services;
using NewsAPI.SharedKernel;
using NUnit.Framework;

namespace NewsAPI.UnitTests.Core.Services.NewsServiceUnitTests;

[TestFixture]
public class AddAsyncUnitTest
{
    private readonly CategoryEntity _category;

    public AddAsyncUnitTest()
    {
        _category = new CategoryEntity { Category = "Sports" };
    }

    [Theory, AutoDomainData]
    public async Task Verify_Added_News_DetailsAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, NewsModel model)
    {
        // Arrange
        mockRepository.Setup(x => x.FirstAsync<CategoryEntity>(x => x.Id == model.CategoryId)).ReturnsAsync(_category);
        mockRepository.Setup(x => x.AddAsync(It.IsAny<NewsEntity>(), It.IsAny<string>())).ReturnsAsync((NewsEntity)model);
        // Act
        var result = await sut.AddAsync(model);
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
    public async Task Verify_That_News_Are_SavedAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, NewsModel model)
    {
        // Arrange
        mockRepository.Setup(x => x.FirstAsync<CategoryEntity>(x => x.Id == model.CategoryId)).ReturnsAsync(_category);
        mockRepository.Setup(x => x.AddAsync(It.IsAny<NewsEntity>(), It.IsAny<string>())).ReturnsAsync((NewsEntity)model);
        // Act
        var result = await sut.AddAsync(model);
        // Assert
        mockRepository.Verify(x => x.SaveChangesAsync(true), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task Verify_Error_Message_When_Category_Does_Not_ExistAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, NewsModel model)
    {
        // Arrange
        mockRepository.Setup(x => x.FirstAsync<CategoryEntity>(x => x.Id == model.CategoryId)).ReturnsAsync((CategoryEntity)null);
        // Act
        var result = await sut.AddAsync(model);
        // Assert
        Assert.AreEqual((int)ErrorCode.CATEGORY_NOT_FOUND, result.Error);
    }
}
