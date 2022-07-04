using System;
using thegame.Models;
using thegame.Services;

namespace thegame.Tests;
using NUnit.Framework;

[TestFixture]
public class Ii_Tests
{
    [Test, Timeout(1000)]
    public void Ii_Works()
    {
        var rnd = new Random();
        var height = 10;
        var width = 12;
        var colorsNumber = 6;
        var cells = new CellDto[height * width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cells[i * width + j] = new CellDto((i * width + j).ToString(), new VectorDto() {X = i, Y = j}, rnd.Next(0, colorsNumber - 1).ToString(),
                    "", 0);
            }
        }
        var color = bot.ChooseBestMove(cells, height, width, colorsNumber);
    }
    
}