namespace OG.DataTypes.Color;

public struct OgRgbaColor(float r, float g, float b, float a)
{
    public float R { get; set; } = r;

    public float G { get; set; } = g;

    public float B { get; set; } = b;

    public float A { get; set; } = a;
}
