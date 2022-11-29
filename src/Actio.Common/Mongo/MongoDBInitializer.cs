using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoDBInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly IMongoDatabase _database;
        private readonly bool _seed;
        private readonly ICustomDatabaseSeeder _customerSeeder;
        public MongoDBInitializer(IMongoDatabase database, 
                        ICustomDatabaseSeeder customerSeeder, 
                        IOptions<MongoDbOptions> DbOptions)
        {
            this._customerSeeder = customerSeeder;
            _database = database;
            _seed = DbOptions.Value.Seed;
        }
        public async Task InitializeAsync<TDocument>()
        {
            if (_initialized) return;

            RegisterDatabaseConventions();

            _initialized = true;

            if (!_seed)
                return;

            await _customerSeeder.SeedDatabase<TDocument>();
        }

        private void RegisterDatabaseConventions()
        {
            ConventionRegistry.Register("MyDatabaseConventions", new ImaginaryDBConventions(), x => true);
        }

        private class ImaginaryDBConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>{
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true)
            };
        }
    }
}