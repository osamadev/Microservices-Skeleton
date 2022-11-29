using Xunit;
using Moq;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using System;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task TestAddAsync()
        {
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var category = "test";
            categoryRepositoryMock.Setup(x  => x.GetAsync(category)).ReturnsAsync(new Category(category));

            var activityService = new ActivityService(categoryRepositoryMock.Object, activityRepositoryMock.Object);
            await activityService.AddActivity(Guid.NewGuid(), Guid.NewGuid(), "Test Activity", 
                "Test Activity Description", category, DateTime.UtcNow);
                
            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}