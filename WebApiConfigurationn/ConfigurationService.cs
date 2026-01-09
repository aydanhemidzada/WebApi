using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.UnitOfWork.Abstract;
using WebApiConfigurationn.DAL.UnitOfWork.Concrete;
using WebApiConfigurationn.Entities.Auth;

namespace WebApiConfigurationn
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddConfigurationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiDbContext>(opt =>
                 opt.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddAutoMapper(typeof(Program).Assembly);


         
            
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddIdentity<AppUser<Guid>, IdentityRole>()
                .AddEntityFrameworkStores<ApiDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                var tokenOption = configuration.GetSection("TokenOptions").Get<TokenOption>();
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOption.Issuer,
                    ValidAudience = tokenOption.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
