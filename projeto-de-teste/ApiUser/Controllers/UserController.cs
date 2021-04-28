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

    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase, IUserController
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<UserReadDto>> getAllUsers()
        {

            List<User> users = _userService.findAll();

            return Ok(_mapper.Map<List<UserReadDto>>(users));

        }

        [HttpGet("{id}", Name = "getUserById")]
        public ActionResult<UserReadDto> getUserById(int id)
        {

            User user = _userService.findById(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserReadDto>(user));


        }

        [HttpPost]
        public ActionResult<UserReadDto> createUser(UserCreateDto userDto)
        {

            User userModelFromRepo = _mapper.Map<User>(userDto);

            if (userDto == null || userDto.Name == null || userDto.Age == 0)
            {
                return BadRequest();
            }



            try
            {
                _userService.save(userModelFromRepo);
            }
            catch
            {
                return BadRequest();
            }

            UserReadDto userReadDto = _mapper.Map<UserReadDto>(userModelFromRepo);

            return CreatedAtRoute(nameof(getUserById), new { Id = userReadDto.Id }, userReadDto);

        }

        [HttpPut("{id}")]
        public ActionResult updateUser(int id, UserUpdateDto userDto)
        {

            User userModelFromRepo = _userService.findById(id);

            if (userModelFromRepo == null)
            {
                return NotFound();
            }
            if (userDto == null)
            {

                return BadRequest();
            }

            _mapper.Map(userDto, userModelFromRepo);



            try
            {
                _userService.update(userModelFromRepo);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult deleteUser(int id)
        {

            User userModelFromRepo = _userService.findById(id);
            if (userModelFromRepo == null)
            {
                return BadRequest();
            }

            try
            {
                _userService.delete(userModelFromRepo);
            }
            catch
            {
                return BadRequest();
            }


            return NoContent();


        }


    }
}