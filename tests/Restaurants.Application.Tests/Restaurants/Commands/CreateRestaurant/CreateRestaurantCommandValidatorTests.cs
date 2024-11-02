using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveVaidationErrors()
        {
            //arrange
            var Command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Description = "Tradiotional",
                Category = "Sea Food",
                HasDelivery =true


            };
            var validator= new CreateRestaurantCommandValidator();

            //act
            var result=validator.TestValidate(Command);
            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}