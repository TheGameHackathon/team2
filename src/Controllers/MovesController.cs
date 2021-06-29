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
        public MovesController(GameBase game, IMapper mapper)
        {
            this.game = game;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
        {
            game.MakeMove(new Vector(userInput.ClickedPos.X, userInput.ClickedPos.Y));
            var gameDto = mapper.Map<GameBase, GameDto>(game);
            return Ok(gameDto);
        }
    }
}