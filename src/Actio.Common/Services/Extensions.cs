using Actio.Common.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Common.Services
{
    public static class Extensions
    {
        public static ServiceHost UseDbInitializer<TDocument>(this ServiceHost serviceHost)
        {
            var serviceScopeFactory = serviceHost.ServiceWebHost.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                scope.ServiceProvider.GetService<IDatabaseInitializer>().InitializeAsync<TDocument>();
            }
            return serviceHost;
        }
    }
}