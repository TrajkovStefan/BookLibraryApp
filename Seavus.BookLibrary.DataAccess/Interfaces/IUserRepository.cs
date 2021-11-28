using Seavus.BookLibrary.Domain.Models;

namespace Seavus.BookLibrary.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Users GetUserByUsername(string username);
        Users LogInUser(string username, string password);
    }
}