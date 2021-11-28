namespace Seavus.BookLibrary.Domain.Models
{
    public partial class ReservationBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual Users User { get; set; }
    }
}