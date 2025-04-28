namespace OG.DataTypes.Rectangle.Float;

public struct OgRectangleF(float x, float y, float width, float height)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;

    public float Width { get; set; } = width;

    public float Height { get; set; } = height;
}