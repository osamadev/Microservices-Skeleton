using System;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Services
{
    public interface IActivityService
    {
        Task AddActivity(Guid id, Guid userId, string name, string categoryName, 
            string description, DateTime createdAt);
    }
}