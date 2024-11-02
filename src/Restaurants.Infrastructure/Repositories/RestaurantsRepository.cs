using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Repository;
using Restaurants.Infrastructure.Pesistence;
using Restaurants.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Common;
using Restaurants.Domain.Constants;
using System.Linq;
using System.Linq.Expressions;
namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {

            // var restaurants = await dbContext.Restaurant
            // .ToListAsync();
            // return restaurants;
            return null;
        }
        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            // Base query with search condition
            var baseQuery = dbContext
                .Restaurant
                .Where(r => searchPhraseLower == null ||
                            r.Name.ToLower().Contains(searchPhraseLower) ||
                            r.Description.ToLower().Contains(searchPhraseLower));

            // Get total count before pagination
            var totalCount = await baseQuery.CountAsync();

            // Sorting
            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>{
            { nameof(Restaurant.Name), r => r.Name },
            { nameof(Restaurant.Description), r => r.Description }
             };

                // Check if the sortBy column exists in the dictionary
                if (columnsSelector.TryGetValue(sortBy, out var selectedColumn))
                {
                    baseQuery = sortDirection == SortDirection.Ascending
                        ? baseQuery.OrderBy(selectedColumn)
                        : baseQuery.OrderByDescending(selectedColumn);  // Use OrderByDescending
                }
            }

            // Pagination
            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }
        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {

            var restaurant = await dbContext.Restaurant
            .Include(r => r.Dish)
            .FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;
        }
        public async Task<int> Create(Restaurant entity)
        {
            dbContext.Restaurant.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }
        public async Task Delete(Restaurant entity)
        {
            dbContext.Restaurant.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            dbContext.SaveChangesAsync();
        }
    }

}