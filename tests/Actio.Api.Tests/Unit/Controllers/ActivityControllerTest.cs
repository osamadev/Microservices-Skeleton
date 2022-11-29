using Actio.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using RawRabbit;
using Actio.Api.Repository;
using System;
using Actio.Common.Commands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivityControllerTest
    {
        [Fact]
        public async Task activity_controller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var activityController = new ActivitiesController(busClientMock.Object, activityRepositoryMock.Object);
            var userId = Guid.NewGuid();
            activityController.ControllerContext = new ControllerContext{
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[]{ new Claim(ClaimTypes.Name, userId.ToString()) }, "test"))
                }

            };
            var command = new CreateActivity{
                Id = Guid.NewGuid(),
                Name = "Test Acivity",
                Description = "Test Activity Description",
                Category = "Sports",
                UserId = userId
            };
            var result = await activityController.Post(command) as AcceptedResult;
            result.Should().NotBeNull();
            result.Location.Should().BeEquivalentTo($"actvities/{command.Id}");
        }
    }
}