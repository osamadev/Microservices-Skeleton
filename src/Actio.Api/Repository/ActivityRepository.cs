using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.DTOs;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Api.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            this._database = database;

        }
        public async Task AddAsync(ActivityDTO activity)
            => await Collection.InsertOneAsync(activity);

        private Func<ActivityDTO, Guid, string, bool> filterActivitiesFunc = (a, userId, category) => {
             if(category != null)
                return (a.UserId == userId && a.Category == category);
            return a.UserId == userId;
        };

        public async Task<IEnumerable<ActivityDTO>> GetAllAsync(Guid userId)
            => await Collection.AsQueryable().Where(a => a.UserId == userId).ToListAsync();

        public async Task<ActivityDTO> GetAsync(Guid activityId)
            => await Collection.AsQueryable().FirstOrDefaultAsync(a => a.Id == activityId);
        private IMongoCollection<ActivityDTO> Collection
            => _database.GetCollection<ActivityDTO>("Activities");
    }
}