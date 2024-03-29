using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.GameEntities;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games/{gameId}/[controller]/[action]")]
public class MovesController : Controller
{
    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody] UserInputDto userInput)
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

        if (userGuid != gameId)
            return Forbid();
        
        if (userInput != null)
        {
            if (userInput.KeyPressed == 'i')
                return MovesOneStepByAI(gameId);
        }

        if (userInput != null)
            Game.MakeStep(gameId, userInput.ClickedPos);
        
        var game = Game.GetMap(gameId, 0);
        return Ok(game);
    }

    [HttpPost]
    public IActionResult MovesOneStepByAI(Guid gameId)
    {
        var huj = Game.MakeStepByBot(gameId);
        var game = Game.GetMapOrDefault(gameId);
        return Ok(game);
    }
}