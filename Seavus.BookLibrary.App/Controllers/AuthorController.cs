using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seavus.BookLibrary.Dtos.AuthorDto;
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
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<AuthorDto>> GetAll()
        {
            try
            {
                Log.Information("Fetching All Authors");
                return _authorService.GetAllAuthors();
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [Authorize(Roles = "RegisteredUser, Admin")]
        [HttpGet("{id}")]
        public ActionResult<UpdateAuthorDto> GetAuthorById(int id)
        {
            try
            {
                Log.Information($"Getting author id {id}");
                return _authorService.GetAuthorById(id);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addAuthor")]
        public IActionResult AddAuthor([FromBody] AddAuthorDto addAuthorDto)
        {
            try
            {
                _authorService.AddAuthor(addAuthorDto);
                Log.Information($"Author {addAuthorDto.FirstName} {addAuthorDto.LastName} was successfully created!");
                return StatusCode(StatusCodes.Status201Created, "Author created successfully");
            }
            catch (AuthorException e)
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

        [Authorize(Roles = "Admin")]
        [HttpPut("updateAuthor")]
        public IActionResult UpdateAuthor([FromBody] AddAuthorDto updateAuthorDto)
        {
            try
            {
                _authorService.UpdateAuthor(updateAuthorDto);
                Log.Information($"Author {updateAuthorDto.FirstName} {updateAuthorDto.LastName} was successfully updated!!");
                return StatusCode(StatusCodes.Status202Accepted, "Author was successfully updated!");
            }
            catch (AuthorException e)
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
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                Log.Information($"Author with id {id} was successfully deleted!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (AuthorException e)
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