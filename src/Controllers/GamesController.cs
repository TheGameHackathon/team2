using System;
using System.Collections.Generic;
using System.Linq;
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
        var guidKey = "UserGuid";
        Guid userGuid;
        if (HttpContext.Request.Cookies.TryGetValue(guidKey, out var cookieGuid))
            userGuid = Guid.Parse(cookieGuid);
        else
        {
            userGuid = Guid.NewGuid();
            HttpContext.Response.Cookies.Append(guidKey, userGuid.ToString());
        }
        
        return Ok(Game.GetMap(userGuid, difficulty));
    }

    [HttpPost("getfield/{gameGuid}")]
    public IActionResult GetField([FromRoute] Guid gameGuid)
    {
        var gameMap = Game.GetMapOrDefault(gameGuid);
        if (gameMap == null)
            return NotFound();
        return Ok(Game.GetMapOrDefault(gameGuid));
    }
}