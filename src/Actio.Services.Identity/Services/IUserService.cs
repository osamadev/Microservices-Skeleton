using System.Threading.Tasks;
using Actio.Common.Auth;

namespace Actio.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterUserAsync(string name, string email, string password);
         Task<JwtToken> LoginAsync(string email, string password);
    }
}