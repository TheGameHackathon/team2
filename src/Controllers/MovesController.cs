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
        throw new NotImplementedException();
        // return Ok(game);
    }
}