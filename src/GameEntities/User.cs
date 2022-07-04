using System;
using thegame.Models;

namespace thegame.GameEntities;

public class User
{
    public User(Guid password, GameDto game)
    {
        Password = password;
        Game = game;
    }

    public GameDto Game { get; set; }
    public Guid Password { get; set; }
}