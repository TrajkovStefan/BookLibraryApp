using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class AuthorBookRepository : IRepository<AuthorBook>
    {
        private BookLibraryAppContext _bookLibraryAppContext;
        public AuthorBookRepository(BookLibraryAppContext bookLibraryAppContext)
        {
            _bookLibraryAppContext = bookLibraryAppContext;
        }
        public void Delete(AuthorBook entity)
        {
            _bookLibraryAppContext.Remove(entity);
            _bookLibraryAppContext.SaveChanges();
        }

        public List<AuthorBook> GetAll()
        {
            return _bookLibraryAppContext.AuthorBook
                                         .Include(x => x.Author)
                                         .Include(y => y.Book)
                                         .ToList();
        }

        public AuthorBook GetById(int id)
        {
            throw new Exception();
        }

        public void Insert(AuthorBook entity)
        {
            _bookLibraryAppContext.AuthorBook.Add(entity);
            _bookLibraryAppContext.SaveChanges();
        }

        public void Update(AuthorBook entity)
        {
            _bookLibraryAppContext.AuthorBook.Update(entity);
            _bookLibraryAppContext.SaveChanges();
        }
    }
}
