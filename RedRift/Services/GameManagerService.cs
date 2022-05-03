using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using RedRift.Data;
using RedRift.Models;
using RedRift.Models.Exceptions;
using RedRift.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RedRift.Services
{
    public class GameManagerService : IGameManagerService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHubContext<GameManagerNotificationsHub, IGameManagerNotifications> hubContext;
        private readonly IGameEmulationService gameEmulationService;

        public GameManagerService(ApplicationDbContext dbContext, IHubContext<GameManagerNotificationsHub, IGameManagerNotifications> hubContext, IGameEmulationService gameEmulationService)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
            this.gameEmulationService = gameEmulationService;
        }

        public async Task<Game> CreateAndJoin(Player player)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Game gm = new Game() { State = GameState.Created, Players = new Player[] { player } };
                    var res = await dbContext.Games.AddAsync(gm);
                    await dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return gm;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Game> JoinAGameAsync(Player player)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var games = dbContext.Games.Where(x => x.Players.Length <2)?.Select(x => x);
                    if (games.Any())
                    {
                        var game = games.OrderByDescending(x=>x.Players.Length).First();
                        game.Players.Append(player);
                        dbContext.Update(player);
                        await dbContext.SaveChangesAsync();
                        transaction.Commit();
                        gameEmulationService.StartGame(game);
                        return game;
                    }
                    else
                        throw new NoGamesFoundException();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            throw new NoGamesFoundException();
        }

        public async Task<Game> JoinAGameAsync(Player player, Game game)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var games = dbContext.Games.Where(x => x.Id == game.Id)?.Select(x => x);
                    if (games.Any())
                    {
                        var targetGame = games.First();
                        if (targetGame.Players.Length > 1)
                            throw new RoomFullException();
                        targetGame.Players.Append(player);
                        dbContext.Update(player);
                        await dbContext.SaveChangesAsync();
                        transaction.Commit();
                        gameEmulationService.StartGame(game);
                        return game;
                    }
                    else
                        throw new NoGamesFoundException();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
            throw new NoGamesFoundException();
        }
    }


}
