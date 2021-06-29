using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services
{
    public class GamesRepo
    {
        public readonly ConcurrentDictionary<Guid, GameDto> Games = new();
    }
}