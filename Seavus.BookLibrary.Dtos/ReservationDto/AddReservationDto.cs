namespace Seavus.BookLibrary.Dtos.ReservationDto
{
    public class AddReservationDto
    {
        public string StartDate { get; set; }
        public int PaymentMethod { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}