namespace OG.DataTypes.Point;

public struct OgPoint3d(int x, int y, int z)
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;

    public int Z { get; set; } = z;
}