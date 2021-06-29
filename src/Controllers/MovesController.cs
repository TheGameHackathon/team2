using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using thegame.Domain;
using thegame.Domain.Game;
using thegame.Models;
using thegame.Services;
using thegame.Services.FieldFactory;

namespace thegame.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        private IMapper mapper;
        private GamesRepo gamesRepo;
        private IFieldFactory factory;
        public MovesController(GamesRepo gamesRepo, IMapper mapper, IFieldFactory factory)
        {
            this.mapper = mapper;
            this.gamesRepo = gamesRepo;
            this.factory = factory;
        }
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
        {
            var gameDto = gamesRepo.Games[gameId];
            var game = mapper.Map<GameDto, SmartGame>(gameDto);
            game.MakeMove(new Vector(userInput.ClickedPos.X, userInput.ClickedPos.Y));
            gamesRepo.Games[gameId].Cells = game.gameField.Cells.SelectMany(x => x.Select(y => new
                CellDto($"h{y.Coordinates.Y}w{y.Coordinates.X}", new VectorDto(y.Coordinates.X, y.Coordinates.Y),
                    y.Color.TypeColorToColor(), "", 0))).ToArray();
            gameDto.Score++;

            if (game.IsFinished)
            {
                gameDto.IsFinished = true;

                return Ok(gameDto);
            }

            return Ok(gameDto);
        }
    }
}