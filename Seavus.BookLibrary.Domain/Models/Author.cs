using System.Collections.Generic;

namespace Seavus.BookLibrary.Domain.Models
{
    public partial class Author
    {
        public Author()
        {
            AuthorBook = new HashSet<AuthorBook>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<AuthorBook> AuthorBook { get; set; }
    }
}
