using OG.DataTypes.ElementAlignment;
using OG.DataTypes.Size;
using OG.DataTypes.Vector;
namespace OG.DataTypes.Rectangle;
public struct OgRectangle(int x, int y, int width, int height)
{
    public OgRectangle(OgVector2 position, OgSize size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }
    public          int X      { get; set; } = x;
    public          int Y      { get; set; } = y;
    public          int Width  { get; set; } = width;
    public          int Height { get; set; } = height;
    public readonly int XMax   => X + Width;
    public readonly int YMax   => Y + Height;
    public OgVector2 Position
    {
        get => new(X, Y);
        set => (X, Y) = (value.X, value.Y);
    }
    public OgSize Size
    {
        get => new(Width, Height);
        set => (X, Y) = (value.Width, value.Height);
    }
    public readonly bool Contains(OgVector2 position) => position.X >= X && position.X < XMax && position.Y >= Y && position.Y < YMax;
    public OgRectangle Align(EOgElementAlignment alignment, OgRectangle parentRect)
    {
        int x = alignment switch
        {
            EOgElementAlignment.UPPER_LEFT or EOgElementAlignment.MIDDLE_LEFT or EOgElementAlignment.LOWER_LEFT => parentRect.X,
            EOgElementAlignment.UPPER_CENTER or EOgElementAlignment.MIDDLE_CENTER or EOgElementAlignment.LOWER_CENTER =>
                parentRect.X + ((parentRect.Width - Width) / 2),
            EOgElementAlignment.UPPER_RIGHT or EOgElementAlignment.MIDDLE_RIGHT or EOgElementAlignment.LOWER_RIGHT =>
                parentRect.X + parentRect.Width - Width,
            _ => parentRect.X
        };
        int y = alignment switch
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