using Seavus.BookLibrary.Dtos.AuthorDto;
using Seavus.BookLibrary.Dtos.BookDto;

namespace Seavus.BookLibrary.Dtos.AuthorBookDto
{
    public class AddAuthorBookDto
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}