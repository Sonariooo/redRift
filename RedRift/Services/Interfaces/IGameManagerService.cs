using RedRift.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RedRift.Services
{
    public interface IGameManagerService
    {
        /// <summary>
        /// Подключиться к случайной игре
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Task<Game> JoinAGameAsync(Player player);
        /// <summary>
        /// На тот случай, если требуется возможность подключаться к конкретной игре, например, через браузер серверов/комнат
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public Task<Game> JoinAGameAsync(Player player,Game game);

        public Task<Game> CreateAndJoin(Player player);
    }
}