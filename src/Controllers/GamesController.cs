using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;
using thegame.Services.FieldFactory;

namespace thegame.Controllers
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            var factory = new FieldFactory().GetGameDto(Complexity.Easy, new []{"color1", "color2", "color3"});
            return Ok(factory);
        }
    }
}
