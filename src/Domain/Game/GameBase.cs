using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public abstract class GameBase
    {
        public readonly GameField gameField;
        public readonly Guid Id;
        public bool IsFinished { get; }
        protected GameBase(Guid id, GameField gameField)
        {
            this.gameField = gameField;
            Id = id;
        }

        public abstract void MakeMove(Vector move);
    }
}
