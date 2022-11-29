using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IActivityRepository _activityRepository;
        public ActivityService(ICategoryRepository categoryRepository,
        IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
            this._categoryRepository = categoryRepository;
            
        }
    public async Task AddActivity(Guid id, Guid userId, string name, string description, 
            string categoryName, DateTime createdAt)
    {
        var category = await _categoryRepository.GetAsync(categoryName);
        if(category == null)
            throw new ActioException("category_is_null", $"Category {categoryName} is not found");

        var activity = new Activity(id, userId, name, description, categoryName, createdAt);
        
        await _activityRepository.AddAsync(activity);
    }
}
}