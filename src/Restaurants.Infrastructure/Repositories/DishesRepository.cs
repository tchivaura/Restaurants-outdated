using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Repository;
using Restaurants.Infrastructure.Pesistence;
using Restaurants.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dish.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id ?? 0;

        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            dbContext.Dish.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}