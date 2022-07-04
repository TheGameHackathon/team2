using System;

namespace thegame.GameEntities;

public enum Colors
{
    Red = 0xFF0000,
    Orange = 0xFF8700,
    LightOrange = 0xFFD300,
    Lime = 0xDEFF0A,
    Green = 0xA1FF0A,
    LightGreen = 0x0AFF99,
    LightBlue = 0x0AEFFF,
    Blue = 0x147DF5,
    DeepBlue = 0x580AFF,
    Magenta = 0xBE0AFF
}

public static class ColorTypeExtensions
{
    public static string ToColor(this Colors colorType)
    {
        return colorType switch
        {
            Colors.Red => "#FF0000",
            Colors.Orange => "#FF8700",
            Colors.LightOrange => "#FFD300",
            Colors.Lime => "#DEFF0A",
            Colors.Green => "#A1FF0A",
            Colors.LightGreen => "#0AFF99",
            Colors.LightBlue => "#0AEFFF",
            Colors.Blue => "#147DF5",
            Colors.DeepBlue => "#580AFF",
            Colors.Magenta => "#BE0AFF",
            _ => throw new ArgumentOutOfRangeException(nameof(colorType), colorType, null)
        };
    }
}