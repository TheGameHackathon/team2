using System;
using System.Xml.Schema;
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
        var height = 5;
        var width = 5;
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

        for (int i = 0; i < width * height; i++)
        {
            var huj = new string[height, width];
            for (int j = 0; j < width * height; j++)
            {
                huj[cells[j].Pos.X, cells[j].Pos.Y] = cells[j].Type;
            }
            var color = Bot.ChooseBestMove(cells, height, width);
            MapHandler.MakeMove(cells, height, width, color);
        }

        for (int i = 0; i < height * width; i++)
        {
            Assert.AreEqual(cells[i].Type, cells[0].Type);
        }
    }
    
}