using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AuthorDto;
using System.Linq;

namespace Seavus.BookLibrary.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto(this Author author)
        {
            return new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Books = author.AuthorBook.Select(x => x.Book.Title).ToList()
            };
        }

        public static Author ToAuthor(this AddAuthorDto addAuthorDto)
        {
            return new Author()
            {
                FirstName = addAuthorDto.FirstName,
                LastName = addAuthorDto.LastName
            };
        }

        public static UpdateAuthorDto ToUpdatedAuthorDto(this Author author)
        {
            return new UpdateAuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }
    }
}