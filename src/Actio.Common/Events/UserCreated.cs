namespace Actio.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Name { get; }
        public string Email { get; }

        protected UserCreated()
        {
            
        }

        public UserCreated(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}