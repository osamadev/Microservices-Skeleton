using Actio.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_get_should_return_content()
        {
            var homeController = new HomeController();
            var result = homeController.Get() as ContentResult;
            result.Should().NotBeNull();
            result.Content.Should().BeEquivalentTo($"Hello from {nameof(HomeController)}");
        }
    }
}