using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using thegame.Models;

namespace thegame.GameEntities;

public class Game
{
    private static readonly Dictionary<Guid, User> Users = new();

    public Game()
    {
    }

    private static CellDto[] GenerateMap(int difficulty)
    {
        var random = new Random();
        var size = CalculateSize(difficulty);
        var values = Enum.GetValues(typeof(Colors));

        var grid =
            from x in Enumerable.Range(0, size - 1)
            from y in Enumerable.Range(0, size - 1)
            select new CellDto((x * size + y).ToString(),
                new VectorDto {X = x, Y = y},
                ((Colors) values.GetValue(random.Next(values.Length))!).ToColor(),
                "",
                0);

        return grid.ToArray();
    }

    public static GameDto GetMap(Guid userId, int difficulty)
    {
        if (Users.TryGetValue(userId, out var user)) return user.Game;

        var guid = Guid.NewGuid();
        var password = Guid.NewGuid();
        var gameDto = new GameDto(
            GenerateMap(difficulty), true, true,
            CalculateSize(difficulty), CalculateSize(difficulty), guid,
            false, difficulty * difficulty, password);

        Users.Add(guid, new User(guid, gameDto));
        return gameDto;
    }

    private static int CalculateSize(int difficulty) => (int) (difficulty * 1.5) + 10;
}