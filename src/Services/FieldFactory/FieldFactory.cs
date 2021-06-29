using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using thegame.Models;

namespace thegame.Services.FieldFactory
{
    public class FieldFactory:IFieldFactory
    {
        private Random rnd;

        public FieldFactory()
        {
            rnd = new Random();
        }

        public GameDto GetGameDto(Complexity complexity, string[] colors)
        {
            var size = GetSize(complexity);
            var cells = GetCell(size, colors);

            var game = new GameDto(cells, false, true, size, size, Guid.NewGuid(), false, 0);
            return game;
        }

        private int GetSize(Complexity complexity)=>
            complexity switch
            {
                Complexity.Easy => 15,
                Complexity.Normal => 20,
                Complexity.Hard => 25,
                _ => throw new ArgumentException("compexity is not avaliable"),
            };

        private CellDto[] GetCell(int size, string[] colors)
        {
            var cells = new CellDto[size * size];
            for (int i = 0; i < size * size; i++)
            {
                var position = new VectorDto(i % size, i / size);
                var color = colors[rnd.Next(0, colors.Length)];
                cells[i] = new CellDto(i.ToString(), position, color, "", 0);
            }

            return cells;
        }
    }
}
