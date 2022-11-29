using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
         Task<Activity> GetAsync(Guid activityId);
         Task<IEnumerable<Activity>> GetAllAsync();
         Task AddAsync(Activity activity);
    }
}