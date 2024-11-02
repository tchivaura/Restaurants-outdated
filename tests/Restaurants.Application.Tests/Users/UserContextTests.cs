using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_AuthenticatedUser_ReturnsCurrentUser()
        {
            //arrange
            var httpContextAccessorMock=new Mock<IHttpContextAccessor>();
            var userContext = new UserContext(httpContextAccessorMock.Object);
            var claims=new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier,"1"),
                new (ClaimTypes.Email,"test@gmail.com"),
                new (ClaimTypes.Role,UserRoles.Admin),
                new (ClaimTypes.Role,UserRoles.User)
            };
            var user= new ClaimsPrincipal(new ClaimsIdentity(claims,"Test"));
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User=user
            });

            //act
            var currentUser= userContext.GetCurrentUser();
            //asset
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@gmail.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);

        }
    }
}