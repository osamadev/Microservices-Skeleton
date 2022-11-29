using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;

namespace Actio.Services.Identity.Domain.Repository
{
    public interface IUserRepository
    {
         Task<User> GetUserAsync(Guid id);
         Task<User> GetUserAsync(string email);
         Task AddUserAsync(User user);
    }
}