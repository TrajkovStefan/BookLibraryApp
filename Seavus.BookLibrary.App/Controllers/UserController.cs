using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seavus.BookLibrary.Dtos.UserDto;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using Serilog;
using System;
using System.Collections.Generic;

namespace Seavus.BookLibrary.App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            try
            {
                Log.Information("Getting All Users");
                return _userService.GetAllUsers();
            }
            catch (UserException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [Authorize(Roles = "Admin, RegisteredUser")]
        [HttpGet("{username}")]
        public ActionResult<UserDto> GetUserByUsername(string username)
        {
            try
            {
                Log.Information($"Getting user with username {username}");
                return _userService.GetUserByUsername(username);
            }
            catch (UserException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [Authorize(Roles = "Admin, RegisteredUser")]
        [HttpGet("userId/{id}")]
        public ActionResult<UpdateUserDto> GetUserById(int id)
        {
            try
            {
                Log.Information($"Getting user with id {id}");
                return _userService.GetUserById(id);
            }
            catch (UserException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [AllowAnonymous]
        [HttpPost("registerUser")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.Register(registerUserDto);
                Log.Information($"User {registerUserDto.FirstName} {registerUserDto.LastName} is registred!");
                return StatusCode(StatusCodes.Status201Created, "User created successfully!");
            }
            catch (UserException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [Authorize(Roles = "Admin, RegisteredUser")]
        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserDto userDto)
        {
            try
            {
                _userService.UpdateUser(userDto);
                Log.Information($"User {userDto.FirstName} {userDto.LastName} was successfully updated!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (UserException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                Log.Information($"User with id {id} was successfully deleted!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (UserException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}