using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedRift.Models;
using RedRift.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RedRift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameManagerController : ControllerBase
    {
        private readonly IGameManagerService gameManagerService;

        public GameManagerController(IGameManagerService gameManagerService)
        {
            this.gameManagerService = gameManagerService;
        }

        [HttpPost("random-game")]
        public async Task<ActionResult<Game>> JoinRandomGame([FromBody]Player player)
        {
           return await gameManagerService.JoinAGameAsync(player);
        }

        [HttpPost("specific-game")]
        public async Task<ActionResult<Game>> JoinSpecificGame([FromBody] JoinRequest joinRequest)
        {
            return await gameManagerService.JoinAGameAsync(joinRequest.Player, joinRequest.Game);
        }

        [HttpPost("create-and-join")]
        public async Task<ActionResult<Game>> CreateAndJoin([FromBody] Player player)
        {
            return await gameManagerService.CreateAndJoin(player);
        }

    }

    public class JoinRequest
    {
        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}
