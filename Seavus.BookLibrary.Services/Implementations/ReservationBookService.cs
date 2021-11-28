using Seavus.BookLibrary.DataAccess.Implementations;
using Seavus.BookLibrary.DataAccess.Interfaces;
using Seavus.BookLibrary.Domain.Models;
using Seavus.BookLibrary.Dtos.ReservationBookDto;
using Seavus.BookLibrary.Mappers;
using Seavus.BookLibrary.Services.Intefaces;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Implementations
{
    public class ReservationBookService : IReservationBookService
    {
        private IRepository<ReservationBook> _reservationBookRepository;
        public ReservationBookService(IRepository<ReservationBook> reservationBookRepository)
        {
            _reservationBookRepository = reservationBookRepository;
        }
        public void MakeReservation(MakeBookReservationDto makeBookReservationDto)
        {
            ReservationBook newReservationBook = makeBookReservationDto.ToReservationBook();
            _reservationBookRepository.Insert(newReservationBook);
        }
    }
}
