using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Person.Data;
using Person.Data.Person.Data;
using Person.Repositories.Interfaces;
using Person.Repositories.Services;

namespace Person
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();


            // Swagger configuration
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tunify API",
                    Version = "v1",
                    Description = "API for managing playlists, songs, and artists in the Tunify Platform"
                });

//                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                {
//                    Name = "Authorization",
//                    Type = SecuritySchemeType.Http,
//                    Scheme = "bearer",
//                    BearerFormat = "JWT",
//                    In = ParameterLocation.Header,
//                    Description = "Please enter user token below."
//                });
//                options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//     }
//});
            });




            // Get the connection string settings 
            string ConnectionStringVar = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(optionsX => optionsX.UseSqlServer(ConnectionStringVar));

            builder.Services.AddScoped<IPerson, PersonService>();

            var app = builder.Build();
            // Add redirection from root URL to Swagger UI
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/TunifySwagger/index.html");
                }
                else
                {
                    await next();
                }
            });




            // Enable Swagger
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/{documentName}/swagger.json";
            });

            // Enable Swagger UI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Tunify API v1");
                options.RoutePrefix = "TunifySwagger";  // Swagger UI at root
            });
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");
            app.MapGet("/newpage", () => "Hello World! from the new page");



            app.Run();
        }
    }
}
