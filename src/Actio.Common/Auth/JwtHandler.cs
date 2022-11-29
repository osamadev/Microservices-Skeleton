using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
    public class JwtHandler : IJwtTokenHandler
    {
        private readonly JwtOptions _options;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            this._options = options.Value;
        }
        public JwtToken Create(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.SecretKey);

            var centuryBegins = new DateTime(1970, 1, 1);
            var tokenExpiry = DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes);
            var expiresTicks = (long)(new TimeSpan(tokenExpiry.Ticks - centuryBegins.Ticks).TotalSeconds);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userId.ToString()) }),
                Issuer = _options.Issuer,
                Audience = "Audience",
                IssuedAt = DateTime.UtcNow,
                Expires = tokenExpiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtToken {
                Token = tokenHandler.WriteToken(token),
                Expires = expiresTicks
            };
        }
    }
}