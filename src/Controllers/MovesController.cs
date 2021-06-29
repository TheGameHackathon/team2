using System;
using System.Linq;
using AutoMapper;
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
        private IMapper mapper;
        private GamesRepo gamesRepo;
        public MovesController(GamesRepo gamesRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.gamesRepo = gamesRepo;
        }
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
        {
            var gameDto = gamesRepo.Games[gameId];
            var game = mapper.Map<GameDto, SmartGame>(gameDto);
            game.MakeMove(new Vector(userInput.ClickedPos.X, userInput.ClickedPos.Y));
            gamesRepo.Games[gameId].Cells = game.gameField.Cells.SelectMany(x => x.Select(y=>new 
                CellDto($"h{y.Coordinates.Y}w{y.Coordinates.X}", new VectorDto(y.Coordinates.X, y.Coordinates.Y),
                    TypeColorToColor(y.Color),"", 0))).ToArray();
            //var newGameDto = mapper.Map<SmartGame, GameDto>(game);
            // gamesRepo.Games[gameId] = newGameDto;
            return Ok(gameDto);
        }
        private string TypeColorToColor(Color color)
        {
            if (color == Color.Blue)
                return "color1";
            if (color == Color.Red)
                return "color2";
            if (color == Color.Green)
                return "color3";
            if (color == Color.Cyan)
                return "color4";
            return "color5";
        }
    }
}