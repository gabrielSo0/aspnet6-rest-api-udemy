using Microsoft.EntityFrameworkCore;
using RestAPI.Model.Context;
using RestAPI.Repository;
using RestAPI.Repository.Interfaces;
using RestAPI.Services;
using RestAPI.Services.Implementations;

namespace RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");
            builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            builder.Services.AddApiVersioning();

            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
