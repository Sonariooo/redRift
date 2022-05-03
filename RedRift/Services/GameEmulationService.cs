using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RedRift.Data;
using RedRift.Models;
using RedRift.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RedRift.Services
{
    public class GameEmulationService : IGameEmulationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHubContext<GameManagerNotificationsHub, IGameManagerNotifications> hubContext;

        public GameEmulationService(ApplicationDbContext dbContext, IHubContext<GameManagerNotificationsHub, IGameManagerNotifications> hubContext)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
        }

        public void StartGame(Game game)
        {
            Task task = Task.Run(()=>GameEmulation(game));
        }

        private void GameEmulation (Game game)
        {
            List<PlayerObject> po = new List<PlayerObject>();
            game.State = GameState.Started;
            dbContext.Games.Update(game);
            _=dbContext.SaveChangesAsync().Result;
            foreach (var p in game.Players)
            {
                po.Add(new PlayerObject() { Id = p.Id });
            }
            bool stillActive = true;
            while (stillActive)
            {
                Thread.Sleep(1000);
                Random r = new Random();
                var i = r.Next(0, 2);
                po[i-1].Health-=i;
                if (po[i - 1].Health <= 0)
                    stillActive = false;
                    EndGame(game,po);
                hubContext.Clients.Group(game.Id.ToString()).PlayerObjectStateUpdate(JsonConvert.SerializeObject(po));
            }
        }

        private async void EndGame(Game game, List<PlayerObject> po)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                game.State = GameState.Finished;
                game.GameResult = new GameResult() { ScoreDescription = JsonConvert.SerializeObject(po), Winner = dbContext.Players.Where(x => x.Id == po.Where(x => x.Health > 0).First().Id).First() };
                await hubContext.Clients.Group(game.Id.ToString()).GameStateUpdate(JsonConvert.SerializeObject(game));
                dbContext.Update(game);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        private class PlayerObject
        {
            public Guid Id;
            public int Health=10;
        }
    }
}
