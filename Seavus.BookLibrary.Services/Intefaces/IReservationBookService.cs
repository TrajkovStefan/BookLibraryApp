using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.ReservationBookDto;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IReservationBookService
    {
        void MakeReservation(MakeBookReservationDto makeBookReservationDto);
    }
}
