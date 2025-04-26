namespace OG.DataTypes.Rectangles;

public struct OgRectangle(int x, int y, int width, int height)
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;

    public int Width { get; set; } = width;

    public int Height { get; set; } = height;
}
