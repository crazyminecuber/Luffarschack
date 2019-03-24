using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomAI1
{
    class DublettException : Exception
    {

        public DublettException(): base()
        {
        }

        public override string ToString()
        {
            return "Elementet som skulle läggas till fanns redan i listan.";
        }
        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public DublettException(string message): base(message)
        {
        }

        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="innerException">Exception inner cause</param>
        public DublettException(string message, Exception innerException): base(message, innerException)
        {
        }

    }
}
