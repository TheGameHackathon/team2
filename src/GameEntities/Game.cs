using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace thegame.GameEntities;

public class Game
{
    public GameDto dto;
    public static Dictionary<Guid, User> users;

    public Game()
    {
        
    }

    private static CellDto[] GenerateMap(int difficulty)
    {
        var random = new Random();
        var size = CalculateSize(difficulty);

        var grid = 
            from x in Enumerable.Range(1, size)
            from y in Enumerable.Range(1, size)
            select new CellDto((x * size + y).ToString(),
                new VectorDto { X = x, Y = y }, 
                random.Next(1, Enum.GetNames(typeof(Colors)).Length).ToString(),
                "",
                0);

        return grid.ToArray();
    }

    public static GameDto GetMap(Guid userId, int difficulty)
    {
        if (users.TryGetValue(userId, out var user)) return user.Game;
        
        var guid = Guid.NewGuid();
        var gameDto = new GameDto(
            GenerateMap(difficulty), true, true,
            CalculateSize(difficulty), CalculateSize(difficulty), guid,
            false, difficulty * difficulty);

        users.Add(guid, new User(guid, gameDto));
        return gameDto;
    }

    private static int CalculateSize(int difficulty) =>
        difficulty + 5;
}
