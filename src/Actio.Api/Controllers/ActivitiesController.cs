using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Actio.Common.Commands;
using RawRabbit;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Actio.Api.Repository;
using System.Linq;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly IActivityRepository _activityRepository;
        public ActivitiesController(IBusClient busClient, IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {
            var currentUserId = Guid.Parse(User.Identity.Name);
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            command.UserId = currentUserId;
            await _busClient.PublishAsync(command);
            return Accepted($"actvities/{command.Id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var currentUserId = Guid.Parse(User.Identity.Name);
            var activity = await _activityRepository.GetAsync(id);
            if(activity == null)
                NotFound();
            if(activity.UserId != currentUserId)
                Unauthorized();
            return Json(activity);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var activities = await _activityRepository.GetAllAsync(userId);
            return Json(activities.Select(a => new {a.Id, a.Name, a.Category}));
        }
    }
}