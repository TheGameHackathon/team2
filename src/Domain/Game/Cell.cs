using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public class Cell
    {
        public readonly Vector Coordinates;
        public Color Color;
        public Cell(Vector coordinates, Color color)
        {
            Coordinates = coordinates;
            Color = color;
        }
    }
}
