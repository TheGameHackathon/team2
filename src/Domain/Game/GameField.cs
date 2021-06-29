using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public class GameField
    {
        public readonly Cell[][] Cells;
        public readonly int Width;
        public readonly int Height;
        public GameField(Cell[][] cells, int width, int height)
        {
            Height = height;
            Width = width;
            Cells = cells;
        }
    }
}
