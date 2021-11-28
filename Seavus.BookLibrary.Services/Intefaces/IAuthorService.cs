using Seavus.BookLibrary.Dtos.AuthorDto;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IAuthorService
    {
        List<AuthorDto> GetAllAuthors();
        UpdateAuthorDto GetAuthorById(int id);
        void AddAuthor(AddAuthorDto addAuthor);
        void UpdateAuthor(AddAuthorDto updateAuthorDto);
        void DeleteAuthor(int id);
    }
}