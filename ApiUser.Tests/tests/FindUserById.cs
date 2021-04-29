using System;
using System.Collections.Generic;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Exceptions;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ApiUser.tests
{
    public class FindUserById
    {
        private readonly IUserController _userController;
        private readonly IUserService _userService;

        public FindUserById()
        {
            this._userService = new UserServiceFake();
            this._userController = new UserController(_userService, new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }


        [Fact]
        public void getUser_WithId_1_ReturnsOkResult()
        {

            var okResult = Assert.IsType<OkObjectResult>(_userController.getUserById(1).Result);
            var user = Assert.IsType<UserReadDto>(okResult.Value);
            var expectedUser = new User(1, "Leonardo", "Mendes", 23, DateTime.Now);
            Assert.Equal(user.Name,expectedUser.Name);
            Assert.Equal(user.Id,expectedUser.Id);
            Assert.Equal(user.Surname,expectedUser.Surname);
            Assert.Equal(user.Age,expectedUser.Age);

        }

        [Fact]
        public void getUserNonexistent_ReturnsNotFound()
        {
            var notFoundResult = _userController.getUserById(4);

            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void getUser_WithNegativeId_ReturnsNotFound()
        {
            var notFoundResult = _userController.getUserById(-4);

            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }


    }
}
