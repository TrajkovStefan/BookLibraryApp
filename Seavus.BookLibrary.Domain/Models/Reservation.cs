using System;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Domain.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationBook = new HashSet<ReservationBook>();
        }

        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<ReservationBook> ReservationBook { get; set; }
    }
}
