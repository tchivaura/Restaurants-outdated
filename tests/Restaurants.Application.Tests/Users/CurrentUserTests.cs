using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests.Users
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { UserRoles.Admin, UserRoles.User });

            //act
            var isInRole = currentUser.IsInRole(roleName);

            // assert
            isInRole.Should().BeTrue();
        }
    }
}
