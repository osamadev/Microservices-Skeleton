using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _database;
        public CategoryRepository(IMongoDatabase database)
        {
            this._database = database;

        }
        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await Collection.AsQueryable().ToListAsync();

        public async Task<Category> GetAsync(string name)
            => await Collection.AsQueryable().FirstOrDefaultAsync(c => c.Name == name);

        public async Task<Category> GetAsync(Guid categoryId)
            => await Collection.AsQueryable().FirstOrDefaultAsync(c => c.Id == categoryId);
        private IMongoCollection<Category> Collection
            => _database.GetCollection<Category>("Categories");
    }
}