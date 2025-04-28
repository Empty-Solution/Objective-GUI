using System;

namespace OG.DataTypes.Vector;

public struct OgVector2(int x, int y) : IEquatable<OgVector2>
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;
    
    public static OgVector2 operator + (OgVector2 left, OgVector2 right) =>
        new(left.X + right.X, left.Y + right.Y);

    public bool Equals(OgVector2 other) => x == other.X && y == other.Y;
}