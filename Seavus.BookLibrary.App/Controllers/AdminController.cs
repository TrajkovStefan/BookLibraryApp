using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Dtos.AdminDto;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Seavus.BookLibrary.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public ActionResult<List<AdminDto>> GetAllAdminsAndUsers()
        {
            try
            {
                var claims = User.Claims;
                string adminId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string adminUsername = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                string adminFullName = User.Claims.First(x => x.Type == "adminFullName").Value;
                if (adminUsername != "petko123")
                {
                    Log.Error("The user can not access all admins and users");
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                Log.Information("Getting all admins and users");
                return _adminService.GetAllAdminsAndUsers();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("registerAdmin")]
        public IActionResult RegisterAdmin([FromBody] RegisterAdminDto registerAdminDto)
        {
            try
            {
                _adminService.Register(registerAdminDto);
                Log.Information($"Registred {registerAdminDto.FirstName} {registerAdminDto.LastName}");
                return StatusCode(StatusCodes.Status201Created, "Admin created successfully!");
            }
            catch (AdminException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("loginAdmin")]
        public IActionResult LogInAdmin([FromBody] LogInAdminDto logInAdminDto)
        {
            try
            {
                string token = _adminService.LogIn(logInAdminDto);
                Log.Information($"Logged {logInAdminDto.Username}");
                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch(ResourceNotFound e)
            {
                Log.Error($"The user could not log in with this username {logInAdminDto.Username} and password {logInAdminDto.Password}");
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("updateAdmin")]
        public IActionResult UpdateAdmin([FromBody] AdminDto adminDto)
        {
            try
            {
                _adminService.UpdateAdmin(adminDto);
                Log.Information($"Admin {adminDto.FirstName} {adminDto.LastName} was successfully updated!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (AdminException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                _adminService.DeleteAdmin(id);
                Log.Information($"Admin with id {id} was successfully deleted!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (AdminException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}