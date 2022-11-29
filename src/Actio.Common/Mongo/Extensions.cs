using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection("mongodb"));
            services.AddSingleton<MongoClient>(c => {
                var options = c.GetService<IOptions<MongoDbOptions>>();  
                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c => {
                var client = c.GetService<MongoClient>();  
                var options = c.GetService<IOptions<MongoDbOptions>>(); 
                return client.GetDatabase(options.Value.Database);
            });

            services.AddScoped<IDatabaseInitializer, MongoDBInitializer>();
            //services.AddScoped<ICustomDatabaseSeeder, MongoSeeder>();
        }
    }
}