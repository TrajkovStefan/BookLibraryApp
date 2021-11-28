using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.BookDto;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IBookService
    {
        List<BookDto> GetAllBooks();
        UpdateBookDto GetBookById(int id);
        void AddBook(AddBookDto addBookDto);
        void UpdateBook(UpdateBookDto updateBookDto);
        void DeleteBook(int id);
        IEnumerable<BookDto> GetAllBooksByUserInput(string title, string genre, string author);
        Author GetAuthorByUserInput(AddBookDto addBookDto);
        Book GetBookByUserInput(AddBookDto addBookDto);
    }
}