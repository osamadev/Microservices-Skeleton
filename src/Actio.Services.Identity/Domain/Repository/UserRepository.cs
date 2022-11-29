using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Identity.Domain.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        public UserRepository(IMongoDatabase database)
        {
            this._database = database;

        }
        public async Task AddUserAsync(User user)
            => await Collection.InsertOneAsync(user);

        public async Task<User> GetUserAsync(string email)
            => await Collection.AsQueryable().FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant());

        public async Task<User> GetUserAsync(Guid userId)
            => await Collection.AsQueryable().FirstOrDefaultAsync(u => u.Id == userId);

        private IMongoCollection<User> Collection
            => _database.GetCollection<User>("Users");
    }
}