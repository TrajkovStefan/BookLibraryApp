using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Dtos.BookDto;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Seavus.BookLibrary.App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<BookDto>> GetAll()
        {
            Log.Information("Fetching all books");
            return _bookService.GetAllBooks();
        }

        [Authorize(Roles = "RegisteredUser, Admin")]
        [HttpGet("{id}")]
        public ActionResult<UpdateBookDto> GetBookById(int id)
        {
            try
            {
                Log.Information($"Fetch book with id {id}");
                return _bookService.GetBookById(id);
            }
            catch
            {
                Log.Error($"Book with {id} not found");
                return NotFound();
            }
        }

        [Authorize(Roles = "RegisteredUser, Admin")]
        [HttpGet("searchBookByInput")]
        public ActionResult<IEnumerable<BookDto>> GetAllBooksByUserInput(string title, string genre, string author)
        {
            try
            {
                IEnumerable<BookDto> searchedBooks = _bookService.GetAllBooksByUserInput(title, genre, author);
                Log.Information($"Search book by user input");
                return searchedBooks.ToList();
            }
            catch (BookException e)
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
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpPost("addBook")]
        public IActionResult AddBook([FromBody] AddBookDto addBookDto)
        {
            try
            {
                _bookService.AddBook(addBookDto);
                Log.Information("The book was successfully added");
                return StatusCode(StatusCodes.Status201Created, "Book created successfully");
            }
            catch (BookException e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
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
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateBook")]
        public IActionResult UpdateBook([FromBody] UpdateBookDto updateBookDto)
        {
            try
            {
                _bookService.UpdateBook(updateBookDto);
                Log.Information($"Book with title {updateBookDto.Title} was successfully updated!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (BookException e)
            {
                Log.Error($"Invalid input from user");
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFound e)
            {
                Log.Error($"Book with id {updateBookDto.Id} was not found!");
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Error($"The book with id {updateBookDto.Id} could not be updated");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured. Please contact your admin");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                Log.Information($"Book was successfully deleted!");
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (ResourceNotFound e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                Log.Error($"The book with id {id} cant be deleted!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}