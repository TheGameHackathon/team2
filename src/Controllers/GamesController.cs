using Microsoft.AspNetCore.Mvc;
using thegame.Domain.Game;
using thegame.Models;
using thegame.Services;
using thegame.Services.FieldFactory;

namespace thegame.Controllers
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        private IFieldFactory fieldFactory;
        private GamesRepo gamesRepo;

        public GamesController(IFieldFactory fieldFactory, GamesRepo gamesRepo)
        {
            this.fieldFactory = fieldFactory;
            this.gamesRepo = gamesRepo;
        }
        [HttpPost]
        public IActionResult Index()
        {
            var newGame = fieldFactory.GetGameDto(Complexity.Level1);
            gamesRepo.Games[newGame.Id] = newGame;
            return Ok(newGame);
        }
    }
}