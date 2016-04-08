using System;

namespace TripCards
{
    /// <summary>
    /// в случае рекурсии
    /// </summary>
    internal class RecursivePathException : Exception
    {
        public RecursivePathException(string message)
            : base(message)
        {

        }
    }
}
