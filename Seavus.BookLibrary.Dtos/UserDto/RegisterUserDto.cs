using Seavus.BookLibrary.Domain.Enums;

namespace Seavus.BookLibrary.Dtos.UserDto
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public bool Status { get; set; }
    }
}