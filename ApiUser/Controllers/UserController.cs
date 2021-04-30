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
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<List<User>> getAllUsers()
        {

            List<User> users = _userService.findAll();
            try
            {
                _logger.LogInformation($"GET:{Request.Path} - 200 OK at {DateTime.Now}");
            }
            catch
            {

            }
            return Ok(_mapper.Map<List<UserReadDto>>(users));

        }

        [HttpGet("{id}", Name = "getUserById")]
        public ActionResult<UserReadDto> getUserById(int id)
        {

            User user = _userService.findById(id);

            if (user == null)
            {
                try{_logger.LogWarning($"GET:{Request.Path} - 404 NotFound at {DateTime.Now}");}
                catch { }
                return NotFound();
            }
            try{_logger.LogInformation($"GET:{Request.Path} - 200 OK at {DateTime.Now}"); }
            catch { }
            return Ok(_mapper.Map<UserReadDto>(user));

        }

        [HttpPost]
        public ActionResult<UserReadDto> createUser(UserCreateDto userDto)
        {

            User userModelFromRepo = _mapper.Map<User>(userDto);

            if (userDto == null || userDto.Name == null || userDto.Age == 0)
            {
                try{_logger.LogWarning($"POST:{Request.Path} (nome={userDto.Name};surname={userDto.Surname};age={userDto.Age}) - 400 BadRequest at {DateTime.Now}");
                     }
                catch { }
                    return BadRequest();
            }

            _userService.save(userModelFromRepo);

            try
            {
                _userService.saveChanges();
            }
            catch (Exception e)
            {
                try
                {
                    _logger.LogWarning($"POST:{Request.Path} (nome={userModelFromRepo.Name};surname={userModelFromRepo.Surname};age={userModelFromRepo.Age}) - 500 Internal server error at {DateTime.Now}, message: {e.Message}");
                }
                catch { }
                return BadRequest();
            }

            UserReadDto userReadDto = _mapper.Map<UserReadDto>(userModelFromRepo);

            try
            {
                _logger.LogInformation($"POST:{Request.Path} (id={userReadDto.Id};nome={userReadDto.Name};surname={userReadDto.Surname};age={userReadDto.Age}) - 201 Created at {DateTime.Now}");
            }
            catch { }

            return CreatedAtRoute(nameof(getUserById), new { Id = userReadDto.Id }, userReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult updateUser(int id, UserUpdateDto userDto)
        {

            User userModelFromRepo = _userService.findById(id);

            if (userModelFromRepo == null)
            {
                try
                {
                    _logger.LogWarning($"PUT:{Request.Path} - 404 NotFound at {DateTime.Now}");
                }
                catch { }
                return NotFound();
            }
            if(userDto == null){
                try
                {
                    _logger.LogWarning($"PUT:{Request.Path} - 400 BadRequest at {DateTime.Now}");
                }
                catch { }
                return BadRequest();
            }

            _mapper.Map(userDto, userModelFromRepo);

            _userService.update(userModelFromRepo);

            try
            {
                _userService.saveChanges();
            }
            catch (Exception e)
            {
                try
                {
                    _logger.LogWarning($"PUT:{Request.Path} - 500 Internal server error at {DateTime.Now}, message: {e.Message}");
                }
                catch { }
                return BadRequest();
            }
            try
            {
                _logger.LogInformation($"PUT:{Request.Path} (nome={userDto.Name};surname={userDto.Surname};age={userDto.Age}) - 200 OK at {DateTime.Now}");
            }
            catch { }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteUser(int id)
        {

            User userModelFromRepo = _userService.findById(id);
            if (userModelFromRepo == null)
            {
                try
                {
                    _logger.LogWarning($"DELETE:{Request.Path} - 400 BadRequest at {DateTime.Now}");
                }
                catch { }
                return BadRequest();
            }
            _userService.delete(userModelFromRepo);
            try
            {
                _userService.saveChanges();
            }
            catch (Exception e)
            {
                try
                {
                    _logger.LogWarning($"PUT:{Request.Path} - 500 Internal server error at {DateTime.Now}, message: {e.Message}");
                }
                catch { }
                return BadRequest();
            }
            try
            {
                _logger.LogWarning($"DELETE:{Request.Path} - 204 NoContent at {DateTime.Now}");
            }
            catch { }

            return NoContent();

        }


    }
}