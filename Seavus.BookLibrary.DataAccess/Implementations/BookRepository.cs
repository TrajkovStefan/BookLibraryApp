using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class BookRepository : /*IRepository<Book>*/ IBookRepository
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public BookRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }

        public void Delete(Book entity)
        {
            _bookLibraryDbContext.Book.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _bookLibraryDbContext.Book
                                        .Include(x => x.AuthorBook)
                                        .ThenInclude(x => x.Author)
                                        //.Take(5).ToList();
                                        .ToList();
        }

        public IEnumerable<Book> GetAllBooksIEnumerable()
        {
            return _bookLibraryDbContext.Book.Where(x => x.Status == true && x.NumOfCopies > 0)
                                        .Include(x => x.AuthorBook)
                                        .ThenInclude(x => x.Author);
        }

        public Book GetById(int id)
        {
            return _bookLibraryDbContext.Book.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Book entity)
        {
            _bookLibraryDbContext.Book.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public void Update(Book entity)
        {
            _bookLibraryDbContext.Book.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}