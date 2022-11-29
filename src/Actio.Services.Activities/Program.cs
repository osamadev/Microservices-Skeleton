using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Services;
using Actio.Services.Activities.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;

namespace Actio.Services.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
           ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateActivity>()
                .Build()
                .UseDbInitializer<Category>()
                .Run();
                
            // BsonClassMap.RegisterClassMap<Activity>(cm => {
            //     cm.AutoMap();
            //     // cm.MapProperty(actvity => actvity.Id);
            //     // cm.MapProperty(actvity => actvity.Name);
            //     // cm.MapProperty(actvity => actvity.Description);
            //     // cm.MapProperty(actvity => actvity.Category);
            //     // cm.MapProperty(actvity => actvity.UserId);
            //     // cm.MapProperty(actvity => actvity.CreatedAt);
            //     // cm.MapCreator(activity => new Activity(activity.Id, activity.UserId, 
            //     //     activity.Name, activity.Description, new Category(activity.Category), activity.CreatedAt));
            // });
        }

        
    }
}
