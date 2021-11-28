using Microsoft.EntityFrameworkCore;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private BookLibraryAppContext _bookLibraryDbContext;
        public UserRepository(BookLibraryAppContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        public void Delete(Users entity)
        {
            _bookLibraryDbContext.Remove(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public List<Users> GetAll()
        {
            return _bookLibraryDbContext.Users.Where(x => x.Role == RoleEnum.RegisteredUser)
                                        .Include(x => x.ReservationBook)
                                        .ThenInclude(x => x.Book)
                                        .ToList();
        }
        public Users GetById(int id)
        {
            return _bookLibraryDbContext.Users.Include(x => x.ReservationBook).FirstOrDefault(x => x.Id == id);
        }

        public Users GetUserByUsername(string username)
        {
            return _bookLibraryDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public void Insert(Users entity)
        {
            _bookLibraryDbContext.Users.Add(entity);
            _bookLibraryDbContext.SaveChanges();
        }

        public Users LogInUser(string username, string password)
        {
            return _bookLibraryDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password && x.Role == RoleEnum.RegisteredUser);
        }

        public void Update(Users entity)
        {
            _bookLibraryDbContext.Users.Update(entity);
            _bookLibraryDbContext.SaveChanges();
        }
    }
}