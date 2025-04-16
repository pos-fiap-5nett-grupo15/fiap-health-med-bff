using Fiap.Health.Med.Bff.Api.Extensions;
using Fiap.Health.Med.Bff.CrossCutting.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices(builder.Configuration);
        builder.Services.Configure<ExternalServicesSettings>(builder.Configuration.GetSection("ServicesSettings"));
        builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection("JwtSettings"));

        var app = builder.Build();
        app.MapHealthChecks("/health");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}