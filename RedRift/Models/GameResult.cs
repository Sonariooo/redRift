using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Models
{
    public class GameResult : IIdentifiable, IServiceFields
    {
        public Guid Id { get; set; }
        public Player Winner { get; set; }
        public string ScoreDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
