using System;
using System.Runtime.CompilerServices;

namespace thegame.Models;

public class VectorDto
{
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        return Equals(obj as VectorDto);
    }

    protected bool Equals(VectorDto other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}