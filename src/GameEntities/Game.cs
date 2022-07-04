using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using thegame.Models;
using thegame.Services;

namespace thegame.GameEntities;

public class Game
{
    private static readonly Dictionary<Guid, GameDto> Users = new();

    public Game()
    {
    }

    private static CellDto[] GenerateMap(int difficulty)
    {
        var random = new Random();
        var size = CalculateSize(difficulty);
        var values = Enum.GetValues(typeof(Colors));

        var grid =
            from x in Enumerable.Range(0, size)
            from y in Enumerable.Range(0, size)
            select new CellDto((x * size + y).ToString(),
                new VectorDto {X = x, Y = y},
                ((Colors) values.GetValue(random.Next(values.Length))!).ToColor(),
                "",
                0);

        return grid.ToArray();
    }

    public static GameDto GetMap(Guid userId, int difficulty)
    {
        if (Users.TryGetValue(userId, out var user)) return user;

        var guid = userId;
        var password = userId;
        var gameDto = new GameDto(
            GenerateMap(difficulty), true, true,
            CalculateSize(difficulty), CalculateSize(difficulty), guid,
            false, (int) Math.Pow(CalculateSize(difficulty), 2), password);

        Users.Add(guid, gameDto);
        return gameDto;
    }

    private static int CalculateSize(int difficulty) => (int) (difficulty * 1.5) + 10;

    public static bool MakeStep(Guid userId, VectorDto vector)
    {
        if (!Users.TryGetValue(userId, out var game)) return false;
        var color = "";
        foreach (var cell in game.Cells)
        {
            if (cell.Pos.Equals(vector))
            {
                color = cell.Type;
                break;
            }
        }
        MapHandler.MakeMove(game.Cells, game.Height, game.Width, color);
        return true;
    }

    private static bool Paint(List<int> job, string oldColor, string newColor, string[,] grid, int width, int height)
    {
        var newJob = new List<int>();

        while (job.Count > 1)
        {
            var y = job.Last();
            job.RemoveAt(job.Count - 1);
            var x = job.Last();
            job.RemoveAt(job.Count - 1);

            if (oldColor != grid[x, y])
                continue;

            grid[x, y] = newColor;

            if (x < width - 1 && grid[x + 1, y] == oldColor)
            {
                newJob.Add(x + 1);
                newJob.Add(y);
            }

            if (y < height - 1 && grid[x, y + 1] == oldColor)
            {
                newJob.Add(x);
                newJob.Add(y + 1);
            }

            if (x > 0 && grid[x - 1, y] == oldColor)
            {
                newJob.Add(x - 1);
                newJob.Add(y);
            }

            if (y > 0 && grid[x, y - 1] == oldColor)
            {
                newJob.Add(x);
                newJob.Add(y - 1);
            }
        }

        if (newJob.Count > 0)
            Paint(newJob, oldColor, newColor, grid, width, height);


        var c = grid[0, 0];
        var win = true;

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (grid[i, j] == c) continue;
                win = false;
                break;
            }
        }

        return win;
    }
}