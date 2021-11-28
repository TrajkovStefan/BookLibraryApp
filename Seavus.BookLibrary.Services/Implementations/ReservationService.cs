using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.ReservationDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;
using Seavus.BookLibrary.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private IRepository<Reservation> _reservationRepository;
        private IRepository<Payment> _paymentRepository;
        private IBookRepository _bookRepository;
        private IRepository<ReservationBook> _reservationBookRepository;
        private IUserRepository _userRepository;
        public ReservationService(IRepository<Reservation> reservationRepository, IRepository<Payment> paymentRepository, IRepository<ReservationBook> reservationBookRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _reservationRepository = reservationRepository;
            _paymentRepository = paymentRepository;
            _reservationBookRepository = reservationBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }
        public void AddReservation(AddReservationDto addReservationDto)
        {
            ValidateReservationInput(addReservationDto);
            Users logedUser = _userRepository.GetById(addReservationDto.UserId);
            Book reservedBook = _bookRepository.GetById(addReservationDto.BookId);
            if (logedUser == null)
            {
                throw new UserException("User not found!");
            }
            if (reservedBook == null)
            {
                throw new BookException("Book not found!");
            }

            Reservation newReservation = addReservationDto.ToReservation();
            _reservationRepository.Insert(newReservation);

            Payment newPayment = addReservationDto.ToPayment();
            _paymentRepository.Insert(newPayment);

            

            ReservationBook newReservationBook = new ReservationBook();

            newReservationBook.ReservationId = newReservation.Id;
            newReservationBook.PaymentId = newPayment.Id;
            newReservationBook.UserId = logedUser.Id;
            newReservationBook.BookId = reservedBook.Id;
            reservedBook.NumOfCopies--;

            newReservation.ReservationBook.Add(newReservationBook);
            newPayment.ReservationBook.Add(newReservationBook);
            logedUser.ReservationBook.Add(newReservationBook);
            reservedBook.ReservationBook.Add(newReservationBook);
            _reservationBookRepository.Insert(newReservationBook);
            _reservationRepository.Update(newReservation);
            _paymentRepository.Update(newPayment);
            _userRepository.Update(logedUser);
            _bookRepository.Update(reservedBook);
        }
        public List<ReservationDto> GetAllReservations()
        {
            List<ReservationDto> allReservations = _reservationBookRepository.GetAll().Select(x => x.ToReservationDto()).ToList();
            return allReservations;
        }
        public List<UserReservationDto> UserReservations(string username)
        {
            Users userDb = _userRepository.GetUserByUsername(username);
            if (userDb == null)
            {
                return null;
            }
            return _reservationBookRepository.GetAll().Where(x => x.User.Username == username).Select(x => x.ToUserReservationDto()).ToList();
        }
        public void ReturnBook(int id)
        {
            ReservationBook reservationBook = _reservationBookRepository.GetById(id);
            if(reservationBook == null)
            {
                throw new ResourceNotFound("Reservation Book not be found!");
            }
            
            Reservation reservationDb = _reservationRepository.GetById(reservationBook.ReservationId);
            if (reservationDb == null)
            {
                throw new ResourceNotFound("Reservation not be found!");
            }
            
            Payment paymentDb = _paymentRepository.GetById(reservationBook.PaymentId);
            if (paymentDb == null)
            {
                throw new ResourceNotFound("Payment not be found!");
            }
            
            Book bookDb = _bookRepository.GetById(reservationBook.BookId);
            if (bookDb == null)
            {
                throw new ResourceNotFound("Book not be found!");
            }

            bookDb.NumOfCopies++;
            if(DateTime.UtcNow > reservationDb.EndDate)
            {
                TimeSpan differenceDays = (TimeSpan)(DateTime.UtcNow - reservationDb.EndDate);
                paymentDb.PenaltyPrice = differenceDays.Days * 50;
            }

            _reservationBookRepository.Delete(reservationBook);
            _reservationRepository.Delete(reservationDb);
            _paymentRepository.Delete(paymentDb);
           
            _bookRepository.Update(bookDb);
        }
        #region private methods
        private void ValidateReservationInput(AddReservationDto addReservationDto)
        {
            if (string.IsNullOrEmpty(addReservationDto.StartDate))
            {
                throw new ReservationException("The start date must be entered!");
            }
        }
        #endregion
    }
}