using Application.Authentication;
using Application.Authentication.Services;
using Application.Category.Services;
using Application.Common.Mapping;
using Application.Interfaces;
using Application.Outcome.Services;
using Application.Product.Services;
using Application.User.Services;
using Domain.Entity;
using Infrastructure;
using Infrastructure.Initialization;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using IAuthenticationService = Application.Interfaces.IAuthenticationService;

namespace WebSkladPetApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var authOptionsConfiguration = builder.Configuration.GetSection("Auth");
            var authOptions = builder.Configuration.GetSection("Auth").Get<AuthenticationOption>();
            builder.Services.Configure<AuthenticationOption>(authOptionsConfiguration);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<WebSkladDbContext>(options => options.UseSqlServer("Data Source=ATOMSKILLS;Initial Catalog=WebSkladPetApi;User ID=sa;Password=6811400;Integrated Security=true;"));
            builder.Services.AddDbContext<WebSkladUsersContext>(options => options.UseSqlServer("Data Source=ATOMSKILLS;Initial Catalog=WebSkladPetUsersApi;User ID=sa;Password=6811400;Integrated Security=true;"));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IOutcomeService, OutcomeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthenticationService, UserAuthenticationService>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userService = services.GetRequiredService<IUserService>();
                    await UserInitialization.Initialize(userService);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
                options.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}