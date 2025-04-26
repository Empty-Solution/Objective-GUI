namespace OG.DataTypes.Size;

public struct OgSize(int width, int height)
{
    public int Width { get; set; } = width;

    public int Height { get; set; } = height;
}
