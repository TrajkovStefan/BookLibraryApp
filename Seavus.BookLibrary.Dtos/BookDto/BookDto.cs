using System.Collections.Generic;

namespace Seavus.BookLibrary.Dtos.BookDto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}