using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.UserDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRepository<ReservationBook> _reservationBookRepository; 
        public UserService(IUserRepository userRepository, IRepository<ReservationBook> reservationBookRepository)
        {
            _userRepository = userRepository;
            _reservationBookRepository = reservationBookRepository;
        }

        public void Register(RegisterUserDto registerUserDto)
        {
            ValidateUser(registerUserDto);
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashedPassword = Encoding.ASCII.GetString(passwordHash);

            Users newUser = registerUserDto.ToUser();
            newUser.Password = hashedPassword;

            _userRepository.Insert(newUser);
        }

        public List<UserDto> GetAllUsers()
        {
            List<Users> userDb = _userRepository.GetAll();
            return userDb.Select(x => x.ToUserDto()).ToList();
        }
        public UserDto GetUserByUsername(string username)
        {
            Users userDb = _userRepository.GetUserByUsername(username);
            return userDb.ToUserDto();
        }
        public UpdateUserDto GetUserById(int id)
        {
            Users userDb = _userRepository.GetById(id);
            return userDb.ToUpdateUserDto();
        }

        public void UpdateUser(UpdateUserDto userDto)
        {
            Users userDb = _userRepository.GetUserByUsername(userDto.Username);
            if (userDb == null)
            {
                throw new Exception($"User with id {userDto.Username} was not found");
            }

            if(userDb.Password != userDto.Password)
            {
                MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

                byte[] passwordBytes = Encoding.ASCII.GetBytes(userDto.Password);
                byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
                string hashedPassword = Encoding.ASCII.GetString(passwordHash);
         
                userDb.Password = hashedPassword;
            }

            userDb.FirstName = userDto.FirstName;
            userDb.LastName = userDto.LastName;
            userDb.Username = userDto.Username;

            _userRepository.Update(userDb);
        }

        public void DeleteUser(int id)
        {
            Users userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new Exception($"User with id {id} was not found");
            }

            List<ReservationBook> allUserReservations = _reservationBookRepository.GetAll().Where(x => x.UserId == id).ToList();
            if(allUserReservations.Count != 0)
            {
                throw new UserException("The user has reservations. Cannot be deleted!");
            }
    
            _userRepository.Delete(userDb);
        }

        #region private methods
        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.FirstName) || string.IsNullOrEmpty(registerUserDto.LastName))
            {
                throw new UserException("FirstName and LastName are required fields!");
            }
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserException("Username and password are required fields!");
            }
            if (string.IsNullOrEmpty(registerUserDto.Role.ToString()))
            {
                throw new UserException("Role is a required field!");
            }
            if (registerUserDto.FirstName.Length > 50)
            {
                throw new UserException("Firstname can contain maximum 50 characters");
            }
            if (registerUserDto.LastName.Length > 50)
            {
                throw new UserException("Lastname can contain maximum 50 characters");
            }
            if (registerUserDto.Username.Length > 30)
            {
                throw new UserException("Username can contain maximum 30 characters");
            }
            if (!IsUsernameUnique(registerUserDto.Username))
            {
                throw new UserException($"A admin with this username {registerUserDto.Username} has already exists!");
            }
            if (!IsPasswordValid(registerUserDto.Password))
            {
                throw new UserException("The password is not complex enough!");
            }
        }

        private bool IsUsernameUnique(string username)
        {
            return _userRepository.GetUserByUsername(username) == null;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
        #endregion
    }
}
