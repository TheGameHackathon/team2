using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public abstract class GameBase
    {
        public GameField gameField;
        public Guid Id;
        public bool IsFinished { get; set; }

        public GameBase()
        {

        }
        protected GameBase(Guid id, GameField gameField)
        {
            this.gameField = gameField;
            Id = id;
        }

        public abstract void MakeMove(Vector move);
    }
}
