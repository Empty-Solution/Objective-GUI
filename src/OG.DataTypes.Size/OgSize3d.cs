namespace OG.DataTypes.Size;
public struct OgSize3d(int length, int width, int height)
{
    public int Length { get; set; } = length;
    public int Width  { get; set; } = width;
    public int Height { get; set; } = height;
}