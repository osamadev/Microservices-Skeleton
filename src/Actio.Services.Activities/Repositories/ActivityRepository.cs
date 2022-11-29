using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            this._database = database;

        }
        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task<IEnumerable<Activity>> GetAllAsync()
            => await Collection.AsQueryable().ToListAsync();

        public async Task<Activity> GetAsync(string name)
            => await Collection.AsQueryable().FirstOrDefaultAsync(a => a.Name == name.ToLowerInvariant());

        public async Task<Activity> GetAsync(Guid activityId)
            => await Collection.AsQueryable().FirstOrDefaultAsync(a => a.Id == activityId);
        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");
    }
}