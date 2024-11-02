using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Domain.Exceptions
{
    public class NotFoundException(string resourceType, string resourceIdentifier)
    : Exception($"{resourceType} with id:{resourceIdentifier} doesnt exist")
    {

    }
}