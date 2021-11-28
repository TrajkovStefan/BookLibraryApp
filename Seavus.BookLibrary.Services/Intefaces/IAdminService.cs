using Seavus.BookLibrary.Dtos.AdminDto;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IAdminService
    {
        void Register(RegisterAdminDto registerAdminDto);
        string LogIn(LogInAdminDto logInAdminDto);
        List<AdminDto> GetAllAdminsAndUsers();
        void UpdateAdmin(AdminDto adminDto);
        void DeleteAdmin(int id);
    }
}
