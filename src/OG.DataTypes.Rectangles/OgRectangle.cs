using OG.DataTypes.Vector;

namespace OG.DataTypes.Rectangle;

public struct OgRectangle(int x, int y, int width, int height)
{
    public int X { get; set; } = x;

    public int Y { get; set; } = y;

    public int Width { get; set; } = width;

    public int Height { get; set; } = height;

    public readonly int XMax => X + Width;

    public readonly int YMax => Y + Height;

    public readonly bool Contains(OgVector2 position) => position.X >= X && position.X < XMax && position.Y >= Y && position.Y < YMax;
}