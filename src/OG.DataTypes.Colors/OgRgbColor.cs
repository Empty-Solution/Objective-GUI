namespace OG.DataTypes.Color;
public struct OgRgbColor(float r, float g, float b)
{
    public float R { get; set; } = r;
    public float G { get; set; } = g;
    public float B { get; set; } = b;
}