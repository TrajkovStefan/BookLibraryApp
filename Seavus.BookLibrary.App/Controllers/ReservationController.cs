using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seavus.BookLibrary.Dtos.ReservationDto;
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
    public class ReservationController : ControllerBase
    {
        private IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<ReservationDto>> GetAllReservations()
        {
            try
            {
                Log.Information("Getting all reservations");
                return _reservationService.GetAllReservations();
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [Authorize(Roles = "RegisteredUser")]
        [HttpGet("{username}")]
        public ActionResult<List<UserReservationDto>> GetUserReservations(string username)
        {

            List<UserReservationDto> userReservations = _reservationService.UserReservations(username);
            if(userReservations.Count == 0)
            {
                Log.Error($"User with username {username} has no reservations");
                return StatusCode(StatusCodes.Status404NotFound, "No reservations found");
            }
            Log.Information($"All reservations for {username}");
            return userReservations;
        }

        [Authorize(Roles = "Admin, RegisteredUser")]
        [HttpPost("addReservation")]
        public IActionResult AddReservation([FromBody] AddReservationDto addReservationDto)
        {
            try
            {
                _reservationService.AddReservation(addReservationDto);
                Log.Information("Reservation created successfully");
                return StatusCode(StatusCodes.Status201Created, "Reservation created successfully");
            }
            catch (ReservationException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult ReturnBook(int id)
        {
            try
            {
                _reservationService.ReturnBook(id);
                Log.Information($"Book with id {id} successfully returned!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (ReservationException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}
