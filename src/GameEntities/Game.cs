using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;
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

    public static GameDto GetMapOrDefault(Guid userId)
    {
        var result = Users.TryGetValue(userId, out var user) ? user : default;
        // if (result == null || CheckGame(result.Cells))
        //     return GetMap(userId);
        
        return result;
    }

    public static GameDto GetMap(Guid userId, int difficulty = 1)
    {
        if (Users.TryGetValue(userId, out var game))
        {
            if (!CheckGame(game.Cells))
                return game;
            
            Users.Remove(userId);
        }
        
        var guid = userId;
        var password = userId;
        var gameDto = new GameDto(
            GenerateMap(difficulty), true, true,
            CalculateSize(difficulty), CalculateSize(difficulty), guid,
            GetGameState(userId), (int) Math.Pow(CalculateSize(difficulty), 2), password);

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
        game.Score -= 1;
        game.IsFinished = CheckGame(game.Cells);
        return true;
    }

    public static bool MakeStepByBot(Guid userId)
    {
        if (!Users.TryGetValue(userId, out var game)) return false;
        MapHandler.MakeMove(game.Cells, game.Height, game.Width, Bot.ChooseBestMove(game.Cells, game.Height, game.Width));
        game.IsFinished = CheckGame(game.Cells);
        return true;
    }
    
    public static bool CheckGame(CellDto[] grid)
    {
        var currentColor = grid[0].Type;
        var win = grid.All(t => t.Type == currentColor);
        return win;
    }

    public static bool GetGameState(Guid userId) => 
        Users.TryGetValue(userId, out var game) && game.IsFinished;
}