using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using ApiUser.Models;
using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using AutoMapper;
using ApiUser.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using ApiUser.Exceptions;

namespace ApiUser.Controllers
{

    public interface IUserController
    {



        [HttpGet]
        public ActionResult<List<UserReadDto>> getAllUsers();

        [HttpGet("{id}", Name = "getUserById")]
        public ActionResult<UserReadDto> getUserById(int id);
        [HttpPost]
        public ActionResult<UserReadDto> createUser(UserCreateDto userDto);

        [HttpPut("{id}")]
        public ActionResult  updateUser(int id, UserUpdateDto userDto);

        [HttpDelete("{id}")]
        public ActionResult deleteUser(int id);


    }
}