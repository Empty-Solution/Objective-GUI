using OG.DataTypes.ElementAlignment;
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
    public OgRectangleF Align(EOgElementAlignment alignment, OgRectangleF parentRect)
    {
        float x = alignment switch
        {
            EOgElementAlignment.UPPER_LEFT or EOgElementAlignment.MIDDLE_LEFT or EOgElementAlignment.LOWER_LEFT => parentRect.X,
            EOgElementAlignment.UPPER_CENTER or EOgElementAlignment.MIDDLE_CENTER or EOgElementAlignment.LOWER_CENTER =>
                parentRect.X + ((parentRect.Width - Width) / 2),
            EOgElementAlignment.UPPER_RIGHT or EOgElementAlignment.MIDDLE_RIGHT or EOgElementAlignment.LOWER_RIGHT =>
                parentRect.X + parentRect.Width - Width,
            _ => parentRect.X
        };
        float y = alignment switch
        {
            EOgElementAlignment.UPPER_LEFT or EOgElementAlignment.UPPER_CENTER or EOgElementAlignment.UPPER_RIGHT => parentRect.Y,
            EOgElementAlignment.MIDDLE_LEFT or EOgElementAlignment.MIDDLE_CENTER or EOgElementAlignment.MIDDLE_RIGHT =>
                parentRect.Y + ((parentRect.Height - Height) / 2),
            EOgElementAlignment.LOWER_LEFT or EOgElementAlignment.LOWER_CENTER or EOgElementAlignment.LOWER_RIGHT =>
                parentRect.Y + parentRect.Height - Height,
            _ => parentRect.Y
        };
        return new(x, y, Width, Height);
    }
}