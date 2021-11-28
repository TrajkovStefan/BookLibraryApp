using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AdminDto;

namespace Seavus.BookLibrary.Mappers
{
    public static class AdminMapper
    {
        public static Users ToAdmin(this RegisterAdminDto registerAdminDto)
        {
            return new Users()
            {
                Id = registerAdminDto.Id,
                FirstName = registerAdminDto.FirstName,
                LastName = registerAdminDto.LastName,
                Username = registerAdminDto.Username,
                Role = RoleEnum.Admin,
                Status = true
            };
        }

        public static AdminDto ToAdminDto(this Users admin)
        {
            return new AdminDto()
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Username = admin.Username
            };
        }
    }
}