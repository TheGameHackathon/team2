using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public static class Bot
{
    public static string ChooseBestMove(CellDto[] cells, int height, int width, int colorsNumber)
    {
        var twoDimensionCellsArray = MapHandler.Get2DCellsArray(cells, height, width);
        var visitedCells = new HashSet<VectorDto>();
        var upperLeftCornerCells =
            MapHandler.GetComponentCells(twoDimensionCellsArray, visitedCells, new VectorDto() {X = 0, Y = 0}, height, width);
        var colorsCount = new Dictionary<string, int>();
        foreach (var cell in upperLeftCornerCells)
        {
            for (int i = 0; i < 4; i++)
            {
                var newCellPosition = new VectorDto() {X = cell.Pos.X + MapHandler.dx[i], Y = cell.Pos.Y + MapHandler.dy[i]};
                if ( MapHandler.IsCellInsideField(newCellPosition, height, width) && !visitedCells.Contains(newCellPosition))
                {
                    colorsCount[twoDimensionCellsArray[newCellPosition.X, newCellPosition.Y].Type] =
                        colorsCount.ContainsKey(twoDimensionCellsArray[newCellPosition.X, newCellPosition.Y].Type)
                            ? colorsCount[twoDimensionCellsArray[newCellPosition.X, newCellPosition.Y].Type] + 1
                            : 1;
                }
            }
        }

        var bestColor = "";
        var bestColorNumber = -1;
        foreach (var color in colorsCount)
        {
            if (color.Value > bestColorNumber)
            {
                bestColor = color.Key;
                bestColorNumber = color.Value;
            }
        }

        return bestColor;
    }
}