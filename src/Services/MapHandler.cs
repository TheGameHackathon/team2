using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public static class MapHandler
{
    public static int[] dx = new int[]{-1, 0, 1, 0};
    public static int[] dy = new int[] {0, -1, 0, 1};
    
    public static CellDto[,] Get2DCellsArray(CellDto[] cells, int height, int width)
    {
        var twoDimensionArray = new CellDto[height, width];
        foreach (var cell in cells)
        {
            twoDimensionArray[cell.Pos.X, cell.Pos.Y] = cell;
        }

        return twoDimensionArray;
    }

    public static bool IsCellInsideField(VectorDto cellPosition, int height, int width)
    {
        return cellPosition.X >= 0 && cellPosition.Y >= 0 && cellPosition.X < height && cellPosition.Y < width;
    }
    
    public static List<CellDto> GetComponentCells(CellDto[,] cells, HashSet<VectorDto> visitedCells, VectorDto cellPosition, int height, int width)
    {
        visitedCells.Add(cellPosition);
        var cellsList = new List<CellDto>();
        cellsList.Add(cells[cellPosition.X, cellPosition.Y]);
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

    public static void MakeMove(CellDto[] cells, int height, int width, string newColor)
    {
        var twoDimensionCells = Get2DCellsArray(cells, height, width);
        foreach (var cell in GetComponentCells(twoDimensionCells, new HashSet<VectorDto>(),
                     new VectorDto() {X = 0, Y = 0}, height, width))
        {
            cell.Type = newColor;
        }
    }
}