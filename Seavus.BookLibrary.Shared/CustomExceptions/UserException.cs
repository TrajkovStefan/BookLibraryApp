using System;

namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }
    }
}
