using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AuthorBookDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class AuthorBookService : IAuthorBookService
    {
        private IRepository<AuthorBook> _authorBookRepository;
        public AuthorBookService(IRepository<AuthorBook> authorBookRepository)
        {
            _authorBookRepository = authorBookRepository;
        }

        public void AddAuthorToBook(AddAuthorBookDto addAuthorBookDto)
        {
            AuthorBook newAuthorBook = addAuthorBookDto.ToAuthorBook();
            _authorBookRepository.Insert(newAuthorBook);
        }
    }
}