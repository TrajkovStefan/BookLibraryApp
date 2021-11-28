using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AuthorBookDto;
using Seavus.BookLibrary.Dtos.BookDto;
using System.Linq;

namespace Seavus.BookLibrary.Mappers
{
    public static class AuthorBookMapper
    {
        public static AuthorBook ToAuthorBook(this AddAuthorBookDto addAuthorBookDto)
        {
            return new AuthorBook()
            {
                BookId = addAuthorBookDto.BookId,
                AuthorId = addAuthorBookDto.AuthorId
            };
        }

        public static BookDto ToBookDto(this AuthorBook authorBook)
        {
            return new BookDto()
            {
                Id = authorBook.Book.Id,
                Title = authorBook.Book.Title,
                Genre = authorBook.Book.Genre.ToString(),
                Description = authorBook.Book.Description,
                Authors = authorBook.Book.AuthorBook.Select(x => $"{authorBook.Author.FirstName} {authorBook.Author.LastName}").ToList()
            };
        }
    }
}