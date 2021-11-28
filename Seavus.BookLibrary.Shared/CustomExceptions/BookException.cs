using System;

namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class BookException : Exception
    {
        public BookException(string message) : base(message)
        {

        }
    }
}
