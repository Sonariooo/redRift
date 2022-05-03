using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Models
{
    interface IIdentifiable
    {
        public Guid Id { get; set; }
    }
}
