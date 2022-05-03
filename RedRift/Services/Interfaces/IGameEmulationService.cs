using RedRift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Services.Interfaces
{
    public interface IGameEmulationService
    {
        public void StartGame(Game game);
    }
}
