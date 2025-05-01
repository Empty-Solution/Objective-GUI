namespace OG.DataTypes.Color;
public struct OgHsvaColor(float h, float s, float v, float a)
{
    public float H { get; set; } = h;
    public float S { get; set; } = s;
    public float V { get; set; } = v;
    public float A { get; set; } = a;
}