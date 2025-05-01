namespace OG.DataTypes.Vector;
public struct OgVector5(int x, int y, int z, int w, int h)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Z { get; set; } = z;
    public int W { get; set; } = w;
    public int H { get; set; } = h;
}