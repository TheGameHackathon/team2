using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public abstract class GameBase
    {
        protected readonly GameField gameField;
        public readonly Guid Id;
        public readonly Complexity Complexity;
        public bool IsFinished { get; }
        protected GameBase(Guid id, GameField gameField, Complexity complexity)
        {
            this.gameField = gameField;
            Id = id;
            Complexity = complexity;
        }

        public abstract void MakeMove(Vector move);
    }
}
