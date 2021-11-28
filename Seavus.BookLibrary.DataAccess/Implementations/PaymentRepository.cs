using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class PaymentRepository : IRepository<Payment>
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public PaymentRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        public void Delete(Payment entity)
        {
            _bookLibraryDbContext.Payment.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public List<Payment> GetAll()
        {
            return _bookLibraryDbContext.Payment
                                 .Include(x => x.ReservationBook)
                                 .ThenInclude(x => x.User)
                                 .Include(x => x.ReservationBook)
                                 .ThenInclude(x => x.Book)
                                 .ToList();
        }

        public Payment GetById(int id)
        {
            return _bookLibraryDbContext.Payment.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Payment entity)
        {
            _bookLibraryDbContext.Payment.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public void Update(Payment entity)
        {
            _bookLibraryDbContext.Payment.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}
