using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class AuthorRepository : IRepository<Author>
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public AuthorRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        public void Delete(Author entity)
        {
            _bookLibraryDbContext.Author.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public List<Author> GetAll()
        {
            return _bookLibraryDbContext.Author
                                 .Include(x => x.AuthorBook)
                                 .ThenInclude(x => x.Book)
                                 .ToList();
        }

        public Author GetById(int id)
        {
            return _bookLibraryDbContext.Author.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Author entity)
        {
            _bookLibraryDbContext.Author.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public void Update(Author entity)
        {
            _bookLibraryDbContext.Author.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}
