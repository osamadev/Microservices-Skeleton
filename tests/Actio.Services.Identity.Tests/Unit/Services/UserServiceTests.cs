using System;
using Actio.Common.Auth;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repository;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Services;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            string name="tester";
            string email = "tester@email.com";
            string password = "secret";
            string salt="saletsample";
            string hash = "hash";
            string token = "token";

            var userRepositoryMock = new Mock<IUserRepository>();
            var encryptorMock = new Mock<IEncryptor>();
            var jwtHandlerMock = new Mock<IJwtTokenHandler>();
            var userService = new UserService(userRepositoryMock.Object, encryptorMock.Object, jwtHandlerMock.Object);
            encryptorMock.Setup(u => u.GetSalt()).Returns(salt);
            encryptorMock.Setup(u => u.GetHash(password, salt)).Returns(hash);
            var user = new User(name, email);
            user.SetPassword(password, encryptorMock.Object);
            userRepositoryMock.Setup(u => u.GetUserAsync(email)).ReturnsAsync(user);
            jwtHandlerMock.Setup(t => t.Create(It.IsAny<Guid>())).Returns(new JwtToken{
                Token = token
            });
            var generatedToken = await userService.LoginAsync(email, password);
            userRepositoryMock.Verify(x => x.GetUserAsync(email), Times.Once);
            generatedToken.Should().NotBeNull();
            generatedToken.Token.Should().BeEquivalentTo(token);
        }
    }
}