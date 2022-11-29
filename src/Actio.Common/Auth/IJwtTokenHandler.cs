using System;

namespace Actio.Common.Auth
{
    public interface IJwtTokenHandler
    {
         JwtToken Create(Guid userId);
    }
}