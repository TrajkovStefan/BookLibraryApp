using System;

namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class AdminException : Exception
    {
        public AdminException(string message) : base(message)
        {

        }
    }
}
