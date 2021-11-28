using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Seavus.BookLibrary.DataAccess;
using Seavus.BookLibrary.DataAccess.Implementations;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Services.Implementations;
using Seavus.BookLibrary.Services.Intefaces;

namespace Seavus.BookLibrary.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection service, string dbConnectingString)
        {
            service.AddDbContext<BookLibraryAppContext>(x => x.UseSqlServer(dbConnectingString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IRepository<Author>, AuthorRepository>();
            services.AddTransient<IRepository<Reservation>, ReservationRepository>();
            services.AddTransient<IRepository<Payment>, PaymentRepository>();
            services.AddTransient<IRepository<AuthorBook>, AuthorBookRepository>();
            services.AddTransient<IRepository<ReservationBook>, ReservationBookRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthorBookService, AuthorBookService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IReservationBookService, ReservationBookService>();
        }
    }
}