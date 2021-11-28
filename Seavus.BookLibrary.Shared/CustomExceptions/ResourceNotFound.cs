using System;

namespace Seavus.BookLibrary.Shared.CustomExceptions
{
    public class ResourceNotFound : Exception
    {
        public ResourceNotFound(string message) : base(message)
        {

        }
    }
}