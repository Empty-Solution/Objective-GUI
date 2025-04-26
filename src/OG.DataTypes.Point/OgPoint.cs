namespace OG.DataTypes.Point;

public struct OgPoint(int x, int y)
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;
}
