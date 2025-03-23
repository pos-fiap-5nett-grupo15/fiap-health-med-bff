using Fiap.Health.Med.Bff.Api.Extensions;
using Fiap.Health.Med.Bff.CrossCutting.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.ConfigureServices();
        builder.Services.Configure<ExternalServicesSettings>(builder.Configuration.GetSection("ServicesSettings"));

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