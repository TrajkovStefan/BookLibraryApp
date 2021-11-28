using Seavus.BookLibrary.Domain.Enums;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.ReservationDto;
using System;

namespace Seavus.BookLibrary.Mappers
{
    public static class ReservationMapper
    {
        public static Reservation ToReservation(this AddReservationDto addReservationDto)
        {
            return new Reservation()
            {
                StartDate = DateTime.Parse(addReservationDto.StartDate),
                EndDate = DateTime.Parse(addReservationDto.StartDate).AddDays(14)
            };
        }

        public static Payment ToPayment(this AddReservationDto addReservationDto)
        {
            return new Payment()
            {
                ReservationPrice = 100,
                PenaltyPrice = 0,
                PaymentMethod = (PaymentEnum)addReservationDto.PaymentMethod
            };
        }

        public static ReservationDto ToReservationDto(this ReservationBook reservationBook)
        {
            return new ReservationDto()
            {
                UserFirstName = reservationBook.User.FirstName,
                UserLastName = reservationBook.User.LastName,
                BookTitle = reservationBook.Book.Title,
                StartDate = reservationBook.Reservation.StartDate.ToString(),
                EndDate = reservationBook.Reservation.EndDate.ToString(),
                PaymentMethod = reservationBook.Payment.PaymentMethod.ToString()
            };
        }

        public static UserReservationDto ToUserReservationDto(this ReservationBook reservationBook)
        {
            return new UserReservationDto()
            {
                Id = reservationBook.Id,
                BookTitle = reservationBook.Book.Title,
                StartDate = reservationBook.Reservation.StartDate.ToString(),
                EndDate = reservationBook.Reservation.EndDate.ToString(),
                PaymentMethod = reservationBook.Payment.PaymentMethod.ToString()
            };
        }
    }
}