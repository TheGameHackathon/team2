using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public static class bot
{
    private static int[] dx = new int[]{-1, 0, 1, 0};
    private static int[] dy = new int[] {0, -1, 0, 1};
    
    private static CellDto[,] Get2DCellsArray(CellDto[] cells, int height, int width)
    {
        var twoDimensionArray = new CellDto[height, width];
        foreach (var cell in cells)
        {
            twoDimensionArray[cell.Pos.X, cell.Pos.Y] = cell;
        }

        return twoDimensionArray;
    }

    private static bool IsCellInsideField(VectorDto cellPosition, int height, int width)
    {
        return cellPosition.X >= 0 && cellPosition.Y >= 0 && cellPosition.X < height && cellPosition.Y < width;
    }
    
    private static List<CellDto> GetComponentCells(CellDto[,] cells, HashSet<VectorDto> visitedCells, VectorDto cellPosition, int height, int width)
    {
        visitedCells.Add(cellPosition);
        var cellsList = new List<CellDto>();
        
        for (int i = 0; i < 4; i++)
        {
            var newCellPositon = new VectorDto() {X = cellPosition.X + dx[i], Y = cellPosition.Y + dy[i]};
            if (IsCellInsideField(newCellPositon, height, width) && !visitedCells.Contains(newCellPositon) &&
                cells[newCellPositon.X, newCellPositon.Y].Type == cells[cellPosition.X, cellPosition.Y].Type)
            {
                foreach (var cell in GetComponentCells(cells, visitedCells, newCellPositon, height, width))
                {       
                    cellsList.Add(cell);
                }
            }
        }
        return cellsList;
    }
    
    public static string ChooseBestMove(CellDto[] cells, int height, int width, int colorsNumber)
    {
        var twoDimensionCellsArray = Get2DCellsArray(cells, height, width);
        var visitedCells = new HashSet<VectorDto>();
        var upperLeftCornerCells =
            GetComponentCells(twoDimensionCellsArray, visitedCells, new VectorDto() {X = 0, Y = 0}, height, width);
        var colorsCount = new Dictionary<string, int>();
        foreach (var cell in upperLeftCornerCells)
        {
            for (int i = 0; i < 4; i++)
            {
                var newCellPosition = new VectorDto() {X = cell.Pos.X + dx[i], Y = cell.Pos.Y + dy[i]};
                if (IsCellInsideField(newCellPosition, height, width) && !visitedCells.Contains(newCellPosition))
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