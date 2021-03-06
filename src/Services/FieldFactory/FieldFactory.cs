using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using thegame.Domain.Game;
using thegame.Models;

namespace thegame.Services.FieldFactory
{
    public class FieldFactory:IFieldFactory
    {
        private string[] colors = new[]
        {
            "color1", "color2", "color3", "color4", "color5"
        };
        private Random rnd;

        public FieldFactory()
        {
            rnd = new Random();
        }

        public GameDto GetGameDto(Complexity complexity)
        {
            var (size, colorsCount) = GetPatameters(complexity);
            var cells = GetCell(size, colors.Take(colorsCount).ToArray());

            var game = new GameDto(cells, false, true, size, size, Guid.NewGuid(), false, 0);
            game.Complexity = complexity;
            return game;
        }

        private (int size, int colorsCount) GetPatameters(Complexity complexity)=>
            complexity switch
            {
                Complexity.Level1 => (4,3),
                Complexity.Level2 => (5,3),
                Complexity.Level3 => (10,4),
                Complexity.Level4 => (12,4),
                Complexity.Level5 => (15,5),
                _ => throw new ArgumentException("compexity is not avaliable"),
            };

        private CellDto[] GetCell(int size, string[] colors)
        {
            var cells = new CellDto[size * size];
            for (int i = 0; i < size * size; i++)
            {
                var position = new VectorDto(i % size, i / size);
                var color = colors[rnd.Next(0, colors.Length)];
                cells[i] = new CellDto($"h{position.Y}w{position.X}", position, color, "", 0);
            }

            return cells;
        }

        public GameDto GetNextLevel(Complexity complexity)
        {
            return complexity switch
            {
                Complexity.Level1 => GetGameDto(Complexity.Level2),
                Complexity.Level2 => GetGameDto(Complexity.Level3),
                Complexity.Level3 => GetGameDto(Complexity.Level4),
                Complexity.Level4 => GetGameDto(Complexity.Level5),
                _ => null,
            };
        }
    }
}
