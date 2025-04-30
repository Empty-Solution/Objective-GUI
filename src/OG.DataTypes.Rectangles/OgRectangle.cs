#region

using OG.DataTypes.Vector;

#endregion

namespace OG.DataTypes.Rectangle;

public struct OgRectangle(int x, int y, int width, int height)
{
    public int X
    {
        get;
        set
        {
            XMin  = value;
            field = value;
        }
    } = x;

    public int Y
    {
        get;
        set
        {
            YMin  = value;
            field = value;
        }
    } = y;

    public int Width
    {
        get;
        set
        {
            XMax  = X + value;
            field = value;
        }
    } = width;

    public int Height
    {
        get;
        set
        {
            YMax  = Y + value;
            field = value;
        }
    } = height;

    public int XMin { get; private set; }
    public int YMin { get; private set; }
    public int XMax { get; private set; }
    public int YMax { get; private set; }

    public bool Contains(OgVector2 position) => position.X >= XMin && position.X < XMax && position.Y >= YMin && position.Y < YMax;
}