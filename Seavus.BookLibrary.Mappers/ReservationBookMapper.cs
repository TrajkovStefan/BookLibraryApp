using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.ReservationBookDto;

namespace Seavus.BookLibrary.Mappers
{
    public static class ReservationBookMapper
    {
        public static ReservationBook ToReservationBook(this MakeBookReservationDto makeBookReservationDto)
        {
            return new ReservationBook()
            {
                UserId = makeBookReservationDto.UserId,
                BookId = makeBookReservationDto.BookId,
                PaymentId = makeBookReservationDto.PaymentId,
                ReservationId = makeBookReservationDto.ReservationId
            };
        }
    }
}