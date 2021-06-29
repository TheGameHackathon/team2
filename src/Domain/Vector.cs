using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain
{
    public class Vector
    {
        public Vector(int x, int y)
            => (X, Y) = (x, y);
        public int X { get; set; }
        public int Y { get; set; }
    }
}
