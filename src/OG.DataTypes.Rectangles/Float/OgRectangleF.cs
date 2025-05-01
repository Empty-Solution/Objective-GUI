using OG.DataTypes.Vector;

namespace OG.DataTypes.Rectangle.Float;

public struct OgRectangleF(float x, float y, float width, float height)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;

    public float Width { get; set; } = width;

    public float Height { get; set; } = height;

    public readonly float XMax => X + Width;

    public readonly float YMax => Y + Height;

    public readonly bool Contains(OgVector2 position) => position.X >= X && position.X < XMax && position.Y >= Y && position.Y < YMax;
}