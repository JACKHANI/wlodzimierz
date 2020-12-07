using System;
using System.Text;
using Application.API.Common.Interfaces;
using Application.API.Storage.Identity.Models;
using Infrastructure.API.Identity;
using Infrastructure.API.Identity.Core;
using Infrastructure.API.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.API
{
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
    {
        // Persistence.

        services.AddDbContext<WlodzimierzContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(InfrastructureDefaults.Database)));

        services.AddScoped<IWlodzimierzContext>(provider => provider.GetService<WlodzimierzContext>()!);

        // Identity.

        services.AddDbContext<WlodzimierzIdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(InfrastructureDefaults.IdentityDatabase)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<WlodzimierzIdentityContext>();

        services.AddAuthorization();
        services.AddScoped<IIdentityService, IdentityService>();

        // Json Web Token.

        var section = configuration.GetSection(InfrastructureDefaults.JwtSection);
        services.Configure<IdentitySettings>(section);
        var appSettings = section.Get<IdentitySettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = appSettings.Issuer,
                ValidAudience = appSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}
}