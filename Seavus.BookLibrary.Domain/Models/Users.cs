using Seavus.BookLibrary.Domain.Enums;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Domain.Models
{
    public partial class Users
    {
        public Users()
        {
            ReservationBook = new HashSet<ReservationBook>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ReservationBook> ReservationBook { get; set; }
    }
}
