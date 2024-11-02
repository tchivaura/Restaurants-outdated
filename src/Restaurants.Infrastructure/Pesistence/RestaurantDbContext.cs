using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Pesistence
{
    public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
    {





        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Dish> Dish { get; set; }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);
            modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dish)
            .WithOne()
            .HasForeignKey(r => r.RestaurantId);

        }

    }
}