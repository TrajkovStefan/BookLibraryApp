using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seavus.BookLibrary.Dtos.AdminDto;
using Seavus.BookLibrary.Services.Implementations;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared;
using Seavus.BookLibrary.Shared.CustomExceptions;

namespace Seavus.BookLibrary.Tests
{
    [TestClass]
    public class AdminServiceTests
    {
        private readonly IAdminService _adminService;
        public AdminServiceTests()
        {
            IOptions<AppSettings> options = Options.Create<AppSettings>(new AppSettings()
            {
                SecretKey = "Our test secret key"
            });
            _adminService = new AdminService(new FakeAdminRepository(), new FakeUserRepository(), options);
        }

        [TestMethod]
        public void Login_should_succeed_and_return_token()
        {
            //Arrange
            string username = "petko123";
            string password = "p123456";

            //Act 
            string result = _adminService.LogIn(new LogInAdminDto { Username = username, Password = password });

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        public void Login_should_throw_exception_on_invlaid_credentials()
        {
            //Arrange
            string username = "testUser";
            string password = "123456test";

            //Act and Assert
            Assert.ThrowsException<ResourceNotFound>(() => _adminService.LogIn(new LogInAdminDto()
            {
                Username = username,
                Password = password
            }));

        }
    }
}
