using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Models.Exceptions
{
    public class RoomFullException : Exception
    {
        public RoomFullException() : base("В комнате нет свободных мест.")
        {
        }
    }
}
