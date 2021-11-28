using Seavus.BookLibrary.Dtos.ReservationDto;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Services.Intefaces
{
    public interface IReservationService
    {
        void AddReservation(AddReservationDto addReservationDto);
        List<ReservationDto> GetAllReservations();
        List<UserReservationDto> UserReservations(string username);
        void ReturnBook(int id);
    }
}