using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Context;
using RawRabbit.vNext;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static ISubscription WithCommandHandlerAsync<TCommand>(this IBusClient busClient, ICommandHandler<TCommand> handler)
            where TCommand:ICommand
            => busClient.SubscribeAsync<TCommand>((msg, msgContext) => handler.HandleAsync(msg, msgContext), 
            cfg => cfg.WithQueue(queue => queue.WithName("Actio-Queue-01")));

        public static ISubscription WithEventHandlerAsync<TEvent>(this IBusClient busClient, IEventHandler<TEvent> handler)
            where TEvent:IEvent
            => busClient.SubscribeAsync<TEvent>((msg, msgContext) => handler.HandleAsync(msg, msgContext), 
            cfg => cfg.WithQueue(queue => queue.WithName("Actio-Queue-01")));

        private static string GetQueueName<T>() 
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("RabbitMq");
            section.Bind(options);
            var client = BusClientFactory.CreateDefault(options);
            services.AddSingleton<IBusClient>(_ => client);
        }

    }
}