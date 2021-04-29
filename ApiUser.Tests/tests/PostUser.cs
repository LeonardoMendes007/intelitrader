using System;
using System.Linq;
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
    public class PostUsers
    {
        private readonly IUserController _userController;
        private readonly IUserService _userService;

        public PostUsers()
        {
            this._userService = new UserServiceFake();
            this._userController = new UserController(_userService, new MapperConfiguration(c => c.AddProfile<ApiUserProfile>()).CreateMapper());

        }

        [Fact]
        public void postUser_ReturnsCreatedResult()
        {

            UserCreateDto user = new UserCreateDto("Gustavo", "Mendes", 19);
            var okResult = Assert.IsType<CreatedAtRouteResult>(_userController.createUser(user).Result);
            int id = Assert.IsType<UserReadDto>(okResult.Value).Id;
            var createdUser = _userService.findById(id);
            Assert.Equal(user.Name, createdUser.Name);
            Assert.Equal(user.Surname, createdUser.Surname);
            Assert.Equal(user.Age, createdUser.Age);
        }

        [Fact]
        public void postNullUser_ReturnBadRequest()
        {
            int before = _userService.findAll().Count;
            UserCreateDto user = null;
            Assert.IsType<BadRequestResult>(_userController.createUser(user).Result);
            int after = _userService.findAll().Count;
            Assert.Equal(before,after);
        }

        [Fact]
        public void postUser_WithNullSurname_ReturnsCreatedResult()
        {

            UserCreateDto user = new UserCreateDto("Gustavo", null, 19);
            var okResult = Assert.IsType<CreatedAtRouteResult>(_userController.createUser(user).Result);
            int id = Assert.IsType<UserReadDto>(okResult.Value).Id;
            var createdUser = _userService.findById(id);
            Assert.Equal(user.Name, createdUser.Name);
            Assert.Null(user.Surname);
            Assert.Null(createdUser.Surname);
            Assert.Equal(user.Age, createdUser.Age);
        }

        [Fact]
        public void postUser_WithNullName_ReturnsBadRequest()
        {

            int before = _userService.findAll().Count;
            UserCreateDto user = new UserCreateDto(null, "mendes", 19);;
            Assert.IsType<BadRequestResult>(_userController.createUser(user).Result);
            int after = _userService.findAll().Count;
            Assert.Equal(before,after);
        }

        [Fact]
        public void postUser_WithNullAge_ReturnsBadRequest()
        {

            int before = _userService.findAll().Count;
            UserCreateDto user = new UserCreateDto("Leon", "mendes", 0);
            Assert.IsType<BadRequestResult>(_userController.createUser(user).Result);
            int after = _userService.findAll().Count;
            Assert.Equal(before,after);
        }


    }
}
