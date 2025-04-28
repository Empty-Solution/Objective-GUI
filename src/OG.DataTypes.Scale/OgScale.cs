namespace OG.DataTypes.Scale;

public struct OgScale(int x, int y)
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;
}