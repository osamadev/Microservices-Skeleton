using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Context;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient busClient,
        IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            this._logger = logger;
            this._activityService = activityService;
            _busClient = busClient;
        }
        public async Task HandleAsync(CreateActivity command, IMessageContext msgContext)
        {
            _logger.LogInformation($"Creating Activity: {command.Name}");
            try
            {
                await _activityService.AddActivity(command.Id,
                        command.UserId, command.Name, command.Description,
                        command.Category, command.CreatedAt);

                await _busClient.PublishAsync(new ActivityCreated(command.Id,
                        command.UserId, command.Name,
                        command.Category, command.Description,
                        command.CreatedAt));
            }
            catch (ActioException ex)
            {
                _logger.LogError(ex.Message);
                await _busClient.PublishAsync(new CreateActivityRejected(ex.Message, ex.Code, command.Id,
                        command.UserId, command.Name, command.CreatedAt));
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex.Message);
                await _busClient.PublishAsync(new CreateActivityRejected(ex.Message, "error", command.Id,
                        command.UserId, command.Name, command.CreatedAt));
            }
        }
    }
}