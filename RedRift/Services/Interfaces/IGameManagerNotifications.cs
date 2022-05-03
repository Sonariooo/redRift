using RedRift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Services.Interfaces
{
    public interface IGameManagerNotifications
    {
        public Task GameStateUpdate(string message);
        public Task PlayerObjectStateUpdate(string message);
    }
}
