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

        public GamesController(IFieldFactory fieldFactory)
        {
            this.fieldFactory = fieldFactory;
        }
        [HttpPost]
        public IActionResult Index()
        {
            var newGame = fieldFactory.GetGameDto(Complexity.Level2);
            return Ok(newGame);
        }
    }
}
