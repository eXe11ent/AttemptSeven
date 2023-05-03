using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerService
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });

            options.EnableAnnotations();


            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }

    public static void UseSwaggerApp(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
        });
    }
    public static void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.
        services.AddMvc();

        // Add Swagger generator
        services.AddSingleton<SwaggerGenerator>();
        services.AddSwaggerGen();
    }
}
