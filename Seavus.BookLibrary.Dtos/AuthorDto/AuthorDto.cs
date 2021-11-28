using System.Collections.Generic;

namespace Seavus.BookLibrary.Dtos.AuthorDto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Books { get; set; }
    }
}
