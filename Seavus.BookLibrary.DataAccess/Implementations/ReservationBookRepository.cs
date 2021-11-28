using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class ReservationBookRepository : IRepository<ReservationBook>
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public ReservationBookRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
       }
        public void Delete(ReservationBook entity)
        {
            _bookLibraryDbContext.ReservationBook.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public List<ReservationBook> GetAll()
        {
           return _bookLibraryDbContext.ReservationBook
                                        .Include(x => x.User)
                                        .ThenInclude(x => x.ReservationBook)
                                        .Include(x => x.Book)
                                        .ThenInclude(x => x.ReservationBook)
                                        .Include(x => x.Reservation)
                                        .ThenInclude(x => x.ReservationBook)
                                        .Include(x => x.Payment)
                                        .ToList();
        }

        public ReservationBook GetById(int id)
        {
            return _bookLibraryDbContext.ReservationBook.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(ReservationBook entity)
        {
            _bookLibraryDbContext.ReservationBook.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public void Update(ReservationBook entity)
        {
            _bookLibraryDbContext.ReservationBook.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}