using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.Domain.Game;
using thegame.Models;

namespace thegame.Services.FieldFactory
{
    public interface IFieldFactory
    {
        public GameDto GetGameDto(Complexity complexity, string[] color);
    }
}
