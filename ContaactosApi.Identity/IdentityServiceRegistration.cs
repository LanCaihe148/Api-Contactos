using ContactsApi.Identity.Models;
using ContactsApi.Identity.Services;
using Contactos.Application.Contracts.Identity;
using Contactos.Application.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApi.Identity
{
    public static class IdentityServiceRegistration 
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<ContactosIdentityDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(ContactosIdentityDbContext).Assembly.FullName)));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ContactosIdentityDbContext>().AddDefaultTokenProviders();
            services.AddTransient<IAuthService, AuthService>();
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
                };
            });
            return services;

        }
    }
}
