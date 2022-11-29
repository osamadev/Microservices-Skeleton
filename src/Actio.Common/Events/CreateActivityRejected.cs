using System;

namespace Actio.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public string Reason {get;}
        public string Code {get;}
        public Guid Id { get;  }
        public Guid UserId { get;  }
        public string Name { get;  }
        public DateTime CreatedAt { get;  }

        protected CreateActivityRejected(){}
        public CreateActivityRejected(string reason, string code, Guid id, Guid userId, string name, DateTime createdAt)
        {
            Reason = reason;
            Code = code;
            Id = id;
            UserId = userId;
            Name = name;
            CreatedAt = createdAt;
        }
    }
}