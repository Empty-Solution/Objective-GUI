using OG.DataTypes.Size.Float;
using OG.DataTypes.Vector;
using OG.DataTypes.Vector.Float;
namespace OG.DataTypes.Rectangle.Float;
public struct OgRectangleF(float x, float y, float width, float height)
{
    public OgRectangleF(OgVector2F position, OgSizeF size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }
    public          float X      { get; set; } = x;
    public          float Y      { get; set; } = y;
    public          float Width  { get; set; } = width;
    public          float Height { get; set; } = height;
    public readonly float XMax   => X + Width;
    public readonly float YMax   => Y + Height;
    public OgVector2F Position
    {
        get => new(X, Y);
        set => (X, Y) = (value.X, value.Y);
    }
    public OgSizeF Size
    {
        get => new(Width, Height);
        set => (X, Y) = (value.Width, value.Height);
    }
    public readonly bool Contains(OgVector2 position) => position.X >= X && position.X < XMax && position.Y >= Y && position.Y < YMax;
}