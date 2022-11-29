using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Moq;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Actio.Services.Activities.Controllers;
using Microsoft.AspNetCore.Hosting;

namespace Actio.Services.Activities.Tests.Integration.Controllers
{
    public class HomeControllerTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;
        public HomeControllerTests()
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _httpClient = _testServer.CreateClient();
        }

        [Fact]
        public async Task home_controller_get_should_return_string_content()
        {
            var response = await _httpClient.GetAsync("/");
            var result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            result.Should().BeEquivalentTo($"Hello from {nameof(HomeController)} - Activity Service");
        }
    }
}