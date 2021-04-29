    using System;
using System.Linq;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ApiUser.tests
{
    public class PutUser
    {
        private readonly IUserController _userController;
        private readonly IUserService _userService;

        public PutUser()
        {
            this._userService = new UserServiceFake();
            this._userController = new UserController(_userService, new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }

        [Fact]
        public void putUser_WithId_1_ReturnsNotContent()
        {
            UserUpdateDto user = new UserUpdateDto("Leon", "Mend", 21);
            Assert.IsType<NoContentResult>(_userController.updateUser(1, user));
            var expectedUser = _userService.findById(1);
            Assert.Equal(user.Name, expectedUser.Name);
            Assert.Equal(user.Surname, expectedUser.Surname);
            Assert.Equal(user.Age, expectedUser.Age);
        }

        [Fact]
        public void putNonexistentUser_ReturnsNotFound()
        {
            UserUpdateDto user = new UserUpdateDto("Leon", "Mend", 21);
            Assert.IsType<NotFoundResult>(_userController.updateUser(4, user));
        }

        [Fact]
        public void putNullUser_ReturnsBadRequest()
        {
            var beforeUser = _userService.findById(1);
            UserUpdateDto user = null;
            Assert.IsType<BadRequestResult>(_userController.updateUser(1, user));
            var afterUser = _userService.findById(1);
            Assert.Equal(beforeUser, afterUser);
        }

        [Fact]
        public void putUser_WithNullSurname_ReturnsNotContent()
        {
            UserUpdateDto user = new UserUpdateDto("Leon", null, 21);
            Assert.IsType<NoContentResult>(_userController.updateUser(1, user));
            var expectedUser = _userService.findById(1);
            Assert.Equal(user.Name, expectedUser.Name);
            Assert.Null(expectedUser.Surname);
            Assert.Equal(user.Age, expectedUser.Age);

        }

    }
}
