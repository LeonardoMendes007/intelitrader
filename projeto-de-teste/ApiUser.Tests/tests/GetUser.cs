
using System.Collections.Generic;
using ApiUser.Controllers;
using ApiUser.Dtos;
using ApiUser.Models;
using ApiUser.Profiles;
using ApiUser.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ApiUser.tests
{
    public class GetUser
    {
        private readonly IUserController _userController;
        private readonly IUserService _userService;

        public GetUser()
        {
            this._userService = new UserServiceFake();
            this._userController = new UserController(_userService, new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }

        [Fact]
        public void getUsers_ReturnsOkResult()
        {
            var okResult = _userController.getAllUsers();

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void getUsers_ReturnsAllItems()
        {

            var okResult = _userController.getAllUsers().Result as OkObjectResult;
            
            var users = Assert.IsType<List<UserReadDto>>(okResult.Value);
            Assert.Equal(3,users.Count);
        }

        


    }
}
