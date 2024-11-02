using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Infrastructure.Pesistence;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Seeders
{
    public class RestaurantSeeder(RestaurantDbContext _dbContext) : IRestaurantSeeder
    {




        public async Task Seed()

        {
            if(_dbContext.Database.GetPendingMigrations().Any())
            {
               await  _dbContext.Database.MigrateAsync();
            }

            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Restaurant.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurant.AddRange(restaurants);
                    await _dbContext.SaveChangesAsync();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Name = "Mantawa",
                Description = "Sea Food",
                Category = "SeaFood",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Harare",
                    Street = "Lavenham",
                    PostalCode = "000"
                }
            },
            new Restaurant
            {
                Name = "Ringo",
                Description = "Sea Food",
                Category = "SeaFood",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Harare",
                    Street = "Lavenham",
                    PostalCode = "000"
                }
            }
        };

            return restaurants;
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
            [
            new(UserRoles.User)
            {
                NormalizedName=UserRoles.User.ToUpper()
            },
            new(UserRoles.Owner)
            {
                NormalizedName=UserRoles.Owner.ToUpper()
            },

            new(UserRoles.Admin)
            {
                NormalizedName=UserRoles.Admin.ToUpper()
            },
            ];
            return roles;

        }
    }


}