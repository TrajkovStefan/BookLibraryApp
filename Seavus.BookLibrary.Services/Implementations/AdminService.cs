using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.AdminDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared;
using Seavus.BookLibrary.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        private IUserRepository _userRepository;
        private IOptions<AppSettings> _options;
        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _options = options;
        }
        public string LogIn(LogInAdminDto logInAdminDto)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(logInAdminDto.Password));
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            Users adminDb = _adminRepository.LogInAdmin(logInAdminDto.Username, hashedPassword);
            Users userDb = _userRepository.LogInUser(logInAdminDto.Username, hashedPassword);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
            if (adminDb == null && userDb == null)
            {
                throw new ResourceNotFound("User not found");
            }
            if(adminDb != null)
            {
                byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

                securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                        new Claim(ClaimTypes.Name, adminDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, adminDb.Id.ToString()),
                        new Claim("adminFullName", $"{adminDb.FirstName} {adminDb.LastName}"),
                        new Claim(ClaimTypes.Role, RoleEnum.Admin.ToString())
                        })
                };
            }

            if(userDb != null)
            {
                byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

                securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                        new Claim(ClaimTypes.Role, RoleEnum.RegisteredUser.ToString())
                        })
                };      
            }
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public void Register(RegisterAdminDto registerAdminDto)
        {
            ValidateAdmin(registerAdminDto);
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerAdminDto.Password);
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashedPassword = Encoding.ASCII.GetString(passwordHash);

            Users newAdmin = registerAdminDto.ToAdmin();
            newAdmin.Password = hashedPassword;

            _adminRepository.Insert(newAdmin);
        }

        public List<AdminDto> GetAllAdminsAndUsers()
        {
            List<Users> adminDb = _adminRepository.GetAll();
            return adminDb.Select(x => x.ToAdminDto()).ToList();
        }

        public void UpdateAdmin(AdminDto adminDto)
        {
            Users adminDb = _adminRepository.GetAdminByUsername(adminDto.Username);
            if (adminDb == null)
            {
                throw new Exception($"Admin with id {adminDto.Username} was not found");
            }

            adminDb.FirstName = adminDto.FirstName;
            adminDb.LastName = adminDto.LastName;
            adminDb.Username = adminDto.Username;

            _adminRepository.Update(adminDb);
        }

        public void DeleteAdmin(int id)
        {
            Users adminDb = _adminRepository.GetById(id);
            if (adminDb == null)
            {
                throw new Exception($"Admin with id {id} was not found");
            }
            _adminRepository.Delete(adminDb);
        }

        #region private methods
        private void ValidateAdmin(RegisterAdminDto registerAdminDto)
        {
            if (string.IsNullOrEmpty(registerAdminDto.FirstName) || string.IsNullOrEmpty(registerAdminDto.LastName))
            {
                throw new AdminException("Firstname and lastname are required fields!");
            }
            if (string.IsNullOrEmpty(registerAdminDto.Username) || string.IsNullOrEmpty(registerAdminDto.Password))
            {
                throw new AdminException("Username and password are required fields!");
            }
            if (string.IsNullOrEmpty(registerAdminDto.Role.ToString()))
            {
                throw new Exception("Role is a required field!");
            }
            if (registerAdminDto.FirstName.Length > 50)
            {
                throw new AdminException("Firstname can contain maximum 50 characters");
            }
            if (registerAdminDto.LastName.Length > 50)
            {
                throw new AdminException("Lastname can contain maximum 50 characters");
            }
            if (registerAdminDto.Username.Length > 30)
            {
                throw new AdminException("Username can contain maximum 30 characters");
            }
            if (!IsUsernameUnique(registerAdminDto.Username))
            {
                throw new AdminException($"A admin with this username {registerAdminDto.Username} has already exists!");
            }
            if (!IsPasswordValid(registerAdminDto.Password))
            {
                throw new AdminException("The password is not complex enough!");
            }
        }

        private bool IsUsernameUnique(string username)
        {
            return _adminRepository.GetAdminByUsername(username) == null;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
        #endregion
    }
}