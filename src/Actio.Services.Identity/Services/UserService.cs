using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repository;
using Actio.Services.Identity.Domain.Services;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;
        private readonly IJwtTokenHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IEncryptor encryptor,
                    IJwtTokenHandler jwtHandler)
        {
            this._jwtHandler = jwtHandler;
            this._encryptor = encryptor;
            this._userRepository = userRepository;
        }
    public async Task<JwtToken> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserAsync(email);
        if (user == null)
            throw new ActioException("non_existing_user", "User doesn't exist.");

        if (user.ValidatePassword(password, _encryptor))
           return _jwtHandler.Create(user.Id);
        else
            throw new ActioException("wrong_password", "The password is incorrect.");
    }

    public async Task RegisterUserAsync(string name, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ActioException("missing_fields", "Mandatory fields are missing.");

        var user = await _userRepository.GetUserAsync(email);
        if (user != null)
            throw new ActioException("existing_user", "Existing user is already registered with the same email.");

        var newUser = new User(email, name);
        newUser.SetPassword(password, _encryptor);
        await _userRepository.AddUserAsync(newUser);
    }
}
}