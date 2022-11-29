using System;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;

namespace Actio.Services.Identity.Domain.Models
{
    public class User 
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        protected User()
        {
            
        }
        public User(string email, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ActioException("name_is_empty", "Name can not be empty.");
            
            if(string.IsNullOrWhiteSpace(email))
                throw new ActioException("email_is_empty", "Email can not be empty.");

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string plainPassword, IEncryptor encryptor)
        {
            if(string.IsNullOrWhiteSpace(plainPassword))
                throw new ActioException("empty_password", "Password can not be empty.");

            Salt = encryptor.GetSalt();
            Password = encryptor.GetHash(plainPassword, Salt);
        }

        public bool ValidatePassword(string plainPassword, IEncryptor encryptor)
        {
            var passwordHash = encryptor.GetHash(plainPassword, Salt);
            return Password.Equals(passwordHash);
        }
    }
}