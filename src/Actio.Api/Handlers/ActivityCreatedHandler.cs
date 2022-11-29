using System;
using System.Threading.Tasks;
using Actio.Api.Repository;
using Actio.Common.Events;
using RawRabbit;
using RawRabbit.Context;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
        }
        public async Task HandleAsync(ActivityCreated @event, IMessageContext messageContext)
        {
            await _activityRepository.AddAsync(new DTOs.ActivityDTO{
                Id = @event.Id,
                Name = @event.Name,
                Category = @event.Category,
                Description = @event.Description,
                UserId = @event.UserId
            });
            Console.WriteLine($"Activity created event with this name {@event.Name}");
        }
    }
}