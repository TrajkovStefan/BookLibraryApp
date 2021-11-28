using Seavus.BookLibrary.Domain.Models;

namespace Seavus.BookLibrary.DataAccess.Interfaces
{
    public interface IAdminRepository : IRepository<Users>
    {
        Users GetAdminByUsername(string username);
        Users LogInAdmin(string username, string password);
    }
}