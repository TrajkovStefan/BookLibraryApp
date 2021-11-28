using Seavus.BookLibrary.Dtos.UserDto;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IUserService
    {
        void Register(RegisterUserDto registerUserDto);
        List<UserDto> GetAllUsers();
        void UpdateUser(UpdateUserDto userDto);
        void DeleteUser(int id);
        UserDto GetUserByUsername(string username);
        UpdateUserDto GetUserById(int id);
    }
}
