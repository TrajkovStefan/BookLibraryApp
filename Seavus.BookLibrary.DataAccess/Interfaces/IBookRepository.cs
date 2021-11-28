using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;

namespace Seavus.BookLibrary.DataAccess.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAllBooksIEnumerable();
    }
}
