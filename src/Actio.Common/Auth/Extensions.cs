using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Actio.Common.Auth
{
    public static class Extension
    {
        public static void AddJwt(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var options = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(options);
            serviceCollection.Configure<JwtOptions>(section);
            serviceCollection.AddSingleton<IJwtTokenHandler, JwtHandler>();
            serviceCollection.AddAuthentication()
                .AddJwtBearer(opt => {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters {
                        ValidIssuer = options.Issuer,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
                    };
                });
        }
    }
}