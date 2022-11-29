using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.DTOs;

namespace Actio.Api.Repository
{
    public interface IActivityRepository
    {
         Task AddAsync(ActivityDTO activityDTO);
         Task<ActivityDTO> GetAsync(Guid activityId);
         Task<IEnumerable<ActivityDTO>> GetAllAsync(Guid userId);
    }
}