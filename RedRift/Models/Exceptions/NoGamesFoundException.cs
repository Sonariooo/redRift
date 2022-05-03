using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Models.Exceptions
{
    public class NoGamesFoundException : Exception
    {
        public NoGamesFoundException() : base("Свободных игр не найдено.")
        {
        }
    }
}
