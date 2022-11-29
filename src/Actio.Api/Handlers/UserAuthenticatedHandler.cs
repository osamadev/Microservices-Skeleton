using System;
using System.Threading.Tasks;
using Actio.Common.Events;
using RawRabbit.Context;

namespace Actio.Api.Handlers
{
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticated>
    {
        public async Task HandleAsync(UserAuthenticated @event, IMessageContext messageContext)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity created event with this name {@event.Email}");
        }
    }
}