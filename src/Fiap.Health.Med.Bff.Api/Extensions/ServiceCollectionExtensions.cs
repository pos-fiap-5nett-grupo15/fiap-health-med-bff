using Fiap.Health.Med.Bff.Application.Handlers;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Bff.Application.Interfaces.Doctor;
using Fiap.Health.Med.Bff.Application.Interfaces.Patient;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Fiap.Health.Med.Bff.Infrastructure.Http.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Fiap.Health.Med.Bff.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddControllers();
            services.AddSwagger();
            services.AddHandlers();
            services.AddAuthentication(configuration);
            services.AddHealthChecks();

            return services;
        }
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Health Med API",
                    Version = "v1",
                    Description = "Heath Med API - FIAP Students Project - Group 15"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insert token following the pattern: \"Bearer your_authentication_token\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    Array.Empty<string>()
                }
            });
                c.EnableAnnotations();
            });

            return services;
        }
        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationHandler, AuthenticationHandler>();
            services.AddScoped<ITokenHandler, Fiap.Health.Med.Bff.Application.Handlers.TokenHandler>();
            services.AddScoped<IApiClient, ApiClientUtils>();
            services.AddScoped<IDoctorHandler, DoctorHandler>();
            services.AddScoped<IPatientHandler, PatientHandler>();
            return services;
        }
        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetRequiredSection("JwtSettings").Get<SecuritySettings>();

            if (jwtSettings is null || string.IsNullOrEmpty(jwtSettings.JwtSecurityKey))
                throw new ArgumentNullException(nameof(jwtSettings));

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.JwtSecurityKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
