using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Seavus.BookLibrary.Tests
{
    public class FakeAdminRepository : IAdminRepository
    {
        private List<Users> users;
        public FakeAdminRepository()
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("p123456"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            users = new List<Users>()
            {
                new Users(){
                    Id = 1,
                    FirstName = "Petko",
                    LastName = "Petkovski",
                    Username = "petko123",
                    Password = hashedPassword,
                    Role = RoleEnum.Admin
                }
            };
        }
        public void Delete(Users entity)
        {
            users.Remove(entity);
        }

        public Users GetAdminByUsername(string username)
        {
            return users.FirstOrDefault(x => x.Username == username);
        }

        public List<Users> GetAll()
        {
            return users;
        }

        public Users GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Users entity)
        {
            users.Add(entity);
        }

        public Users LogInAdmin(string username, string hashedPassword)
        {
            return users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);
        }

        public void Update(Users entity)
        {
            Users userDb = users.FirstOrDefault(x => x.Id == entity.Id);
            int index = users.IndexOf(userDb);
            users[index] = entity;
        }
    }
}
