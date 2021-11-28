using System;
using System.Collections.Generic;
using System.Text;

namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class AuthorBookException : Exception
    {
        public AuthorBookException(string message) : base(message)
        {

        }
    }
}
