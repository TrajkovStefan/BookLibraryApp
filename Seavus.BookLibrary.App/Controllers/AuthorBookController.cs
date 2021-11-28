using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seavus.BookLibrary.Dtos.AuthorBookDto;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
namespace Seavus.BookLibrary.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        private IAuthorBookService _authorBookService;
        public AuthorBookController(IAuthorBookService authorBookService)
        {
            _authorBookService = authorBookService;
        }

        [HttpPost("addAuthorToBook")]
        public IActionResult AddAuthorsToBook([FromBody] AddAuthorBookDto addAuthorBookDto)
        {
            try
            {
                _authorBookService.AddAuthorToBook(addAuthorBookDto);
                return StatusCode(StatusCodes.Status201Created, "Author added successfully to Book");
            }
            catch (AuthorBookException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}