using EvolveDb;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestAPI.Model.Context;
using RestAPI.Repository;
using RestAPI.Repository.Generic;
using RestAPI.Services;
using RestAPI.Services.Implementations;
using Serilog;
using System.Net.Http.Headers;

namespace RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var appName = "REST APIs From Zero to Azure WIth ASPNET Core 6 and docker";
            var appVersion = "v1";
            var appDescription = "API Restful developed in course";

            // Making swagger endpoints lowercase
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            // Add services to the container.
            builder.Services.AddControllers();

            var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");
            builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            if(builder.Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            builder.Services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
            }).AddXmlSerializerFormatters();

            builder.Services.AddApiVersioning();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = appName,
                        Version = appVersion,
                        Description = $"{appDescription} {appName}",
                        Contact = new OpenApiContact
                        {
                            Name = "Gabriel",
                            Url = new Uri("http://google.com")
                        }
                    });
            });
            
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            // Generate the json for documentation
            app.UseSwagger();

            // Generate the html UI page to acess on browser
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    $"{appName} - {appVersion}");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySqlConnection(connection);
                var evolve = new Evolve(evolveConnection, Log.Information)
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
