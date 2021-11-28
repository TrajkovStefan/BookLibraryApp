using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public ReservationRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        public void Delete(Reservation entity)
        {
            _bookLibraryDbContext.Reservation.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public List<Reservation> GetAll()
        {
            return _bookLibraryDbContext.Reservation
                                 .Include(x => x.ReservationBook)
                                 .ThenInclude(x => x.Book)
                                 .Include(x => x.ReservationBook)
                                 .ThenInclude(x => x.User)
                                 .Include(x => x.ReservationBook)
                                 .ThenInclude(x => x.Payment)
                                 .ToList();
        }

        public Reservation GetById(int id)
        {
            return _bookLibraryDbContext.Reservation.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Reservation entity)
        {
            _bookLibraryDbContext.Reservation.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public void Update(Reservation entity)
        {
            _bookLibraryDbContext.Reservation.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}