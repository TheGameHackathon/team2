using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Domain;
using thegame.Domain.Game;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        private GameBase game;
        public MovesController(GameBase game)
        {
            this.game = game;
        }
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
        {
            game.MakeMove(new Vector(userInput.ClickedPos.X, userInput.ClickedPos.Y));
            return null;
            //var game = TestData.AGameDto(userInput.ClickedPos ?? new VectorDto(1, 1));
            //if (userInput.ClickedPos != null)
            //    game.Cells.First(c => c.Type == "color4").Pos = userInput.ClickedPos;
            //return Ok(game);
        }

        private GameBase DtoToGameBase(GameDto gameDto)
        {
            var game = new SmartGame(gameDto.Id, new GameField());
        }

        private Cell[][] OneDimensionCellsToTwoDimenstion(Cell[] cells, int width, int height)
        {
            var newCells = new Cell[height][];
            for (var i = 0; i < width; i++)
                newCells = new Cell[width]();
        }
    }
}