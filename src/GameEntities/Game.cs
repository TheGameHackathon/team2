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

    private CellDto[] GenerateMap(int difficulty)
    {
        var random = new Random();
        var size = difficulty + 5;

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
}

public class User
{
    public Guid token { get; set; }
    public Guid password { get; set; }
}
