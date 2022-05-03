using Microsoft.AspNetCore.SignalR;
using RedRift.Models;
using RedRift.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedRift.Services
{
    public class GameManagerNotificationsHub : Hub<IGameManagerNotifications>
    {
        /// <summary>
        ///     Subscribe to Game notifications
        /// </summary>
        public async Task SubscribeToGameUpdates(Game game)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{game.Id}");
        }
    }
}
