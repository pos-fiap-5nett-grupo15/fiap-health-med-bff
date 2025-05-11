using Fiap.Health.Med.Bff.Application.Handlers;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Validators;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Validators;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Validators;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Validators;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Validators;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestScheduleToPatient;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestScheduleToPatient.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Validators;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Bff.Application.Interfaces.Doctor;
using Fiap.Health.Med.Bff.Application.Interfaces.Patient;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.HttpClients;
using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Fiap.Health.Med.Bff.Infrastructure.Http.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TokenHandler = Fiap.Health.Med.Bff.Application.Handlers.TokenHandler;

namespace Fiap.Health.Med.Bff.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddControllers();
            services.AddSwagger();
            services.AddHttpServices();
            services.AddValidators();
            services.AddHandlers();
            services.AddAuthentication(configuration);
            services.AddHealthChecks();
            services.AddHttpClientScheduleManagerAPI(configuration);
            services.AddHttpClientUserManagerAPI(configuration);

            return services;
        }

        private static void AddHttpClientScheduleManagerAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var scheduleServiceBaseUrl = configuration.GetSection("ServicesSettings:ScheduleService")[nameof(ScheduleServiceSettings.BaseURL)];
            services.AddHttpClient<IHttpClientScheduleManagerAPI, HttpClientScheduleManagerAPI>()
                .ConfigureHttpClient(
                    x => x.BaseAddress = new Uri(scheduleServiceBaseUrl ?? throw new ArgumentNullException(nameof(scheduleServiceBaseUrl))));
        }

        private static void AddHttpClientUserManagerAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var userServiceBaseUrl = configuration.GetSection("ServicesSettings:UserService")[nameof(UserServiceSettings.BaseURL)];
            services.AddHttpClient<IHttpClientUserManagerAPI, HttpClientUserManagerAPI>()
                .ConfigureHttpClient(
                    x => x.BaseAddress = new Uri(userServiceBaseUrl ?? throw new ArgumentNullException(nameof(userServiceBaseUrl))));
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FIAP Health & Med - BFF API",
                    Version = "v1",
                    Description = "FIAP Health & Med: BFF API - FIAP Students Project - Group 15"
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
            services
                .AddScoped<IAuthenticationHandler, AuthenticationHandler>()
                .AddScoped<ITokenHandler, TokenHandler>()
                .AddScoped<IApiClient, ApiClientUtils>()
                .AddScoped<IDoctorHandler, DoctorHandler>()
                .AddScoped<IPatientHandler, PatientHandler>()
                .AddScoped<IDeclineScheduleByDoctorHandler, DeclineScheduleByDoctorHandler>()
                .AddScoped<IAcceptScheduleByDoctorHandler, AcceptScheduleByDoctorHandler>()
                .AddScoped<IDeletePatientByIdHandler, DeletePatientByIdHandler>()
                .AddScoped<IUpdatePatientByIdHandler, UpdatePatientByIdHandler>()
                .AddScoped<IUpdateScheduleHandler, UpdateScheduleHandler>()
                .AddScoped<ICreateScheduleHandler, CreateScheduleHandler>()
                .AddScoped<IGetScheduleByIdHandler, GetScheduleByIdHandler>()
                .AddScoped<IGetScheduleByDoctorIdHandler, GetScheduleByDoctorIdHandler>()
                .AddScoped<IGetScheduleByPatientIdHandler, GetScheduleByPatientIdHandler>()
                .AddScoped<IRequestScheduleToPatientHandler, RequestScheduleToPatientHandler>()
                .AddScoped<IRequestPatientCancelScheduleHandler, RequestPatientCancelScheduleHandler>()
                .AddScoped<IGetAllSchedulesHandler, GetAllSchedulesHandler>();
                .AddScoped<IUpdatePatientByIdHandler, UpdatePatientByIdHandler>()
                .AddScoped<IUpdateScheduleHandler, UpdateScheduleHandler>()
                .AddScoped<ICreateScheduleHandler, CreateScheduleHandler>()
                .AddScoped<IGetScheduleByIdHandler, GetScheduleByIdHandler>()
                .AddScoped<IGetScheduleByDoctorIdHandler, GetScheduleByDoctorIdHandler>()
                .AddScoped<IGetScheduleByPatientIdHandler, GetScheduleByPatientIdHandler>()
                .AddScoped<IGetDoctorsByFiltersHandler, GetDoctorsByFiltersHandler>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services
                .AddSingleton<IValidator<DeletePatientByIdHandlerRequest>, DeletePatientByIdHandlerValidator>()
                .AddSingleton<IValidator<UpdatePatientByIdHandlerRequest>, UpdatePatientByIdHandlerValidator>()
                .AddSingleton<IValidator<UpdateScheduleHandlerRequest>, UpdateScheduleHandlerValidator>()
                .AddSingleton<IValidator<CreateScheduleHandlerRequest>, CreateScheduleHandlerValidator>()
                .AddSingleton<IValidator<RequestPatientCancelScheduleHandlerRequest>, RequestPatientCancelScheduleHandlerValidator>();
                .AddSingleton<IValidator<UpdatePatientByIdHandlerRequest>, UpdatePatientByIdHandlerValidator>()
                .AddSingleton<IValidator<UpdateScheduleHandlerRequest>, UpdateScheduleHandlerValidator>()
                .AddSingleton<IValidator<CreateScheduleHandlerRequest>, CreateScheduleHandlerValidator>()
                .AddSingleton<IValidator<GetDoctorsByFiltersHandlerRequest>, GetDoctorsByFiltersHandlerValidator>();
            return services;
        }

        private static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services
                .AddScoped<IScheduleManagerService, ScheduleManagerService>()
                .AddScoped<IUserManagerService, UserManagerService>();
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
