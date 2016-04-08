using System;

namespace TripCards
{
    /// <summary>
    /// в случае разрывов в пути
    /// </summary>
    internal class InvalidPathException : Exception
    {
        public InvalidPathException(string message)
            : base(message)
        {

        }
    }
   
}
