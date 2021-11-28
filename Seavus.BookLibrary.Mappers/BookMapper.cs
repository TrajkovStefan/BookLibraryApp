using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AuthorDto;
using Seavus.BookLibrary.Dtos.BookDto;
using System.Linq;

namespace Seavus.BookLibrary.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Authors = book.AuthorBook.Select(x => $"{x.Author.FirstName} {x.Author.LastName}").ToList(),
                Genre = book.Genre.ToString(),
                Description = book.Description
            };
        }

        public static Book ToBook(this AddBookDto addBookDto)
        {
            return new Book
            {
                Title = addBookDto.Title,
                Genre = (RoleGenre)addBookDto.Genre,
                Description = addBookDto.Description,
                Status = bool.Parse(addBookDto.Status),
                NumOfCopies = addBookDto.NumOfCopies
            };
        }

        public static Author ToAuthor(this AddBookDto addBookDto)
        {
            return new Author
            {
                FirstName = addBookDto.FirstName,
                LastName = addBookDto.LastName
            };
        }

        public static UpdateBookDto ToUpdatedBookDto(this Book book)
        {
            return new UpdateBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Genre = (int)book.Genre,
                Status = book.Status.ToString(),
                NumOfCopies = book.NumOfCopies
            };
        }

        public static AuthorBook ToAuthorBook(this Book book)
        {
            return new AuthorBook
            {
                Id = book.Id,
                Book = book
            };
        }
    }
}