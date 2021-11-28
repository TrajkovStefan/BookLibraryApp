using System;

namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class AuthorException : Exception
    {
        public AuthorException(string message) : base(message)
        {

        }
    }
}
