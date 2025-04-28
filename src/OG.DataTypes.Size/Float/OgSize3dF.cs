namespace OG.DataTypes.Size.Float;

public struct OgSize3dF(float length, float width, float height)
{
    public float Length { get; set; } = length;

    public float Width { get; set; } = width;

    public float Height { get; set; } = height;
}