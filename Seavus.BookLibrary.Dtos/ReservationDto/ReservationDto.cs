namespace Seavus.BookLibrary.Dtos.ReservationDto
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string BookTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}