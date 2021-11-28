using System;


namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class ReservationException : Exception
    {
        public ReservationException(string message) : base(message)
        {
            
        }
    }
}
