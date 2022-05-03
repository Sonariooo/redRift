using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Models
{
    public class Game : IIdentifiable, IServiceFields
    {
        public Guid Id { get; set; }
        public Player[] Players { get; set; }
        public GameState State { get; set; }
        public GameResult? GameResult { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }

    public enum GameState
    {
        Created,
        Started,
        Finished
    }
}
