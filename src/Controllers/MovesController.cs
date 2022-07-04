using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games/{gameId}/[controller]/[action]")]
public class MovesController : Controller
{
    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
    {
        if (userInput.KeyPressed == 'i')
            return MovesOneStepByAI(gameId);
        var game = TestData.AGameDto(userInput.ClickedPos ?? new VectorDto {X = 1, Y = 0});
        if (userInput.ClickedPos != null)
            game.Cells.First(c => c.Type == "#ffffff").Pos = userInput.ClickedPos;
        return Ok(game);
    }
    
    [HttpPost]
    public IActionResult MovesOneStepByAI(Guid gameId)
    {
        throw new NotImplementedException();
        // return Ok(game);
    }
}
