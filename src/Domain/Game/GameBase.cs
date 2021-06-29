using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public abstract class GameBase
    {
        protected readonly GameField gameField;
        public bool IsFinished { get; }
        public GameBase(GameField gameField)
        {
            this.gameField = gameField;
        }

        public abstract void MakeMove(Vector move);
    }
}
