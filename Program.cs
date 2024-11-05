
using DafTask.Data;
using DafTask.Models;
using DafTask.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DafTask
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connection = builder.Configuration.GetConnectionString("connection") ?? 
                throw new ArgumentNullException("Failed to connect db");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            var identityBuilder = builder.Services.AddIdentity<UserProfile,  IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            
            builder.Services.AddAuthentication();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var userManager = services.GetRequiredService<UserManager<UserProfile>>();
                var context = services.GetRequiredService<AppDbContext>();

                await context.Database.MigrateAsync();
                await UserProfileSeed.SeedUsersAsync(userManager, context);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            app.Run();
        }
    }
}
