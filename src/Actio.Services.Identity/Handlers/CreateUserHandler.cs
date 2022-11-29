using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repository;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Context;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler: ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IBusClient busClient,
                IUserService userService, ILogger<CreateUserHandler> logger)
        {
            this._logger = logger;
            this._userService = userService;
            _busClient = busClient;
        }
        public async Task HandleAsync(CreateUser command, IMessageContext msgContext)
        {
            _logger.LogInformation($"Creating User: {command.Name} - {command.Email}");
            
            try
            {
                await _userService.RegisterUserAsync(command.Name, command.Email, command.Password);

                await _busClient.PublishAsync(new UserCreated(command.Name, command.Email));
            }
            catch (ActioException ex)
            {
                _logger.LogError(ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(ex.Message, ex.Code, command.Email));
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(ex.Message, "error", command.Email));
            }
        }

        
    }
}