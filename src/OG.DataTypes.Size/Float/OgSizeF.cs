namespace OG.DataTypes.Size.Float;

public struct OgSizeF(float width, float height)
{
    public float Width { get; set; } = width;

    public float Height { get; set; } = height;
}