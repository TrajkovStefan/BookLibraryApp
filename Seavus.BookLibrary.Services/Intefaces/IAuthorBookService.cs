using Seavus.BookLibrary.Dtos.AuthorBookDto;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IAuthorBookService
    {
        void AddAuthorToBook(AddAuthorBookDto addAuthorBookDto);
    }
}