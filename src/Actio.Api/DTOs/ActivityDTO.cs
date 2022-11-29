using System;

namespace Actio.Api.DTOs
{
    public class ActivityDTO
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string Category { get;  set; }
        public Guid UserId { get;  set; }
        public string Description { get;  set; }
    }
}