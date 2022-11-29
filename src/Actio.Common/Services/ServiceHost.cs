using System;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Mongo;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private IWebHost _webHost;

        public IWebHost ServiceWebHost{get{return _webHost;}}
        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }
        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup:class
        {
            Console.Title = typeof(TStartup).Namespace;

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost webHost;
        private IBusClient _bus;

        public HostBuilder(IWebHost webHost)
        {
            this.webHost = webHost;
        }

        public BusBuilder UseRabbitMq()
        {
            _bus = (IBusClient)webHost.Services.GetService(typeof(IBusClient));
            return new BusBuilder(webHost, _bus);
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(webHost);
        }
    }

    public class BusBuilder : BuilderBase
    {
        private IWebHost webHost;
        private IBusClient bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            this.webHost = webHost;
            this.bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var serviceScopeFactory = webHost.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handler = (ICommandHandler<TCommand>)scope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));
                bus.WithCommandHandlerAsync<TCommand>(handler);
            }
            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var serviceScopeFactory = webHost.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handler = (IEventHandler<TEvent>)scope.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));
                bus.WithEventHandlerAsync<TEvent>(handler);
            }

            return this;
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(webHost);
        }
    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }
}