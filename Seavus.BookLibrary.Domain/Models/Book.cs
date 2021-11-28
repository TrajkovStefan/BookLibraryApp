using Seavus.BookLibrary.Domain.Enums;
using System.Collections.Generic;

namespace Seavus.BookLibrary.Domain.Models
{
    public partial class Book
    {
        public Book()
        {
            AuthorBook = new HashSet<AuthorBook>();
            ReservationBook = new HashSet<ReservationBook>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public RoleGenre Genre { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int NumOfCopies { get; set; }

        public virtual ICollection<AuthorBook> AuthorBook { get; set; }
        public virtual ICollection<ReservationBook> ReservationBook { get; set; }
    }
}