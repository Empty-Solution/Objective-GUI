namespace OG.DataTypes.Vector;

public struct OgVector4(int x, int y, int z, int w)
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;

    public int Z { get; set; } = z;

    public int W { get; set; } = w;
}