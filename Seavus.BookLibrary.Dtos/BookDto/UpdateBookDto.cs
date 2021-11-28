namespace Seavus.BookLibrary.Dtos.BookDto
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Genre { get; set; }
        public string Status { get; set; }
        public int NumOfCopies { get; set; }
    }
}