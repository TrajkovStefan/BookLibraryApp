namespace Seavus.BookLibrary.Dtos.PaymentDto
{
    public class AddPaymentDto
    {
        public int? BookPrice { get; set; }
        public int PenaltyPrice { get; set; }
        public int ReservationPrice { get; set; }
        public int PaymentMethod { get; set; }
    }
}