using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCards
{
    /// <summary>
    /// карточка путешествия
    /// </summary>
    internal class Card
    {
        /// <summary>
        /// город отправления
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// город назначения
        /// </summary>
        public string To { get; set; }
    }
}
