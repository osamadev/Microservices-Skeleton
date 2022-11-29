using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Domain.Models;
using MongoDB.Driver;
using System.Linq;
using System.Diagnostics;

namespace Actio.Services.Activities.Services
{
    public class ActivityDatabaseSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;
        public ActivityDatabaseSeeder(IMongoDatabase database, ICategoryRepository categoryRepository) : base(database)
        {
            this._categoryRepository = categoryRepository;
        }
        public override async Task CustomSeedAsync()
        {
            var categories = new List<string>() {
                "Sports",
                "Economy",
                "Politics",
                "Foreign Affairs"
            };
            await Task.WhenAll(categories.Select(x => 
                _categoryRepository.AddAsync(new Category(x))
            ));
        }
    }
}