using System;
using Microsoft.AspNetCore.Mvc;
using thegame.GameEntities;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games")]
public class GamesController : Controller
{
    [HttpPost("{difficulty}")]
    public IActionResult Index([FromRoute] int difficulty)
    {
        return Ok(Game.GetMap(Guid.NewGuid(), difficulty));
    }
}