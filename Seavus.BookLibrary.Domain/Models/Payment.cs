using Seavus.BookLibrary.Domain.Enums;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Domain.Models
{
    public partial class Payment
    {
        public Payment()
        {
            ReservationBook = new HashSet<ReservationBook>();
        }

        public int Id { get; set; }
        public int? BookPrice { get; set; }
        public int PenaltyPrice { get; set; }
        public int ReservationPrice { get; set; }
        public PaymentEnum PaymentMethod { get; set; }

        public virtual ICollection<ReservationBook> ReservationBook { get; set; }
    }
}