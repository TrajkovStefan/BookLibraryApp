namespace Seavus.BookLibrary.Dtos.ReservationBookDto
{
    public class MakeBookReservationDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int PaymentId { get; set; }
        public int ReservationId { get; set; }
    }
}
