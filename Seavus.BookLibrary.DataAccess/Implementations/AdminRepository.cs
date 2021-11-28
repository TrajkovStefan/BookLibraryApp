using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public AdminRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        public void Delete(Users entity)
        {
            _bookLibraryDbContext.Users.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public Users GetAdminByUsername(string username)
        {
            return _bookLibraryDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public List<Users> GetAll()
        {
            return _bookLibraryDbContext.Users/*.Where(x => x.Role == RoleEnum.Admin)*/
                                 .Include(x => x.ReservationBook)
                                 .ThenInclude(x => x.Book)
                                 .ThenInclude(x => x.ReservationBook)
                                 .ThenInclude(x => x.Reservation)
                                 .ThenInclude(x => x.ReservationBook)
                                 .ThenInclude(x => x.Payment)
                                 .ToList();
        }

        public Users GetById(int id)
        {
            return _bookLibraryDbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Users entity)
        {
            _bookLibraryDbContext.Users.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public Users LogInAdmin(string username, string password)
        {
            return _bookLibraryDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password && x.Role == RoleEnum.Admin);
        }

        public void Update(Users entity)
        {
            _bookLibraryDbContext.Users.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}