using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games")]
public class GamesController : Controller
{
    [HttpPost("{difficulty}")]
    public IActionResult Index(int lvl)
    {
        return Ok(TestData.AGameDto(new VectorDto {X = 1, Y = 1}));
    }
}