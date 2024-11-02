using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repository
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);
    }
}