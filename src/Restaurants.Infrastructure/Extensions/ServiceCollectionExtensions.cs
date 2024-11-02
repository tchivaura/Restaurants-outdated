using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Pesistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Domain.Repository;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Pesistence;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantDb");
            //services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer((connectionString), b => b.MigrationsAssembly("Restaurants.API")).EnableSensitiveDataLogging()));
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("Restaurants.API");
                })
                .EnableSensitiveDataLogging()); // Enables detailed logging of sensitive data
            services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantDbContext>();
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddAuthorizationBuilder()
            .AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));

        }
    }
}