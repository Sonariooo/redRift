using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Models
{
    interface IServiceFields
    {
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
