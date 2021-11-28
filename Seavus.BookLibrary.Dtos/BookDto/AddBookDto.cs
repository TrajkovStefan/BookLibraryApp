namespace Seavus.BookLibrary.Dtos.BookDto
{
    public class AddBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Genre { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int NumOfCopies { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}