using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : ICustomDatabaseSeeder
    {
        protected readonly IMongoDatabase _database;
        public MongoSeeder(IMongoDatabase database)
        {
            this._database = database;
        }
        public async Task SeedDatabase<TDocument>()
        {
            if(_database.GetCollection<TDocument>("Categories").AsQueryable().Any())
                return;

            await CustomSeedAsync();
        }

        public virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}