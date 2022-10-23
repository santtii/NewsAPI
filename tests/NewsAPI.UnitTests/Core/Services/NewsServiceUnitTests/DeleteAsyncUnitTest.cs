using AutoFixture.NUnit3;
using Moq;
using NewsAPI.Core.Entities;
using NewsAPI.Core.Interfaces.Data;
using NewsAPI.Core.Services;
using NUnit.Framework;

namespace NewsAPI.UnitTests.Core.Services.NewsServiceUnitTests;

[TestFixture]
public class DeleteAsyncUnitTest
{
    public DeleteAsyncUnitTest()
    {
    }

    [Theory, AutoDomainData]
    public async Task Verify_That_News_Are_DeletedAsync([Frozen] Mock<IRepository> mockRepository, NewsService sut, int newsId)
    {
        // Act
        var result = await sut.DeleteAsync(newsId);
        // Assert
        mockRepository.Verify(x => x.Delete<NewsEntity>(newsId));
        mockRepository.Verify(x => x.SaveChangesAsync(true), Times.Once);
    }
}
