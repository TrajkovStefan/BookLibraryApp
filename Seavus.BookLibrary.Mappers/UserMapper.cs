using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.UserDto;

namespace Seavus.BookLibrary.Mappers
{
    public static class UserMapper
    {
        public static Users ToUser(this RegisterUserDto registerUserDto)
        {
            return new Users()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Role = RoleEnum.RegisteredUser,
                Status = true
            };
        }
        
        public static UserDto ToUserDto(this Users user)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };
        }

        public static UpdateUserDto ToUpdateUserDto(this Users user)
        {
            return new UpdateUserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}