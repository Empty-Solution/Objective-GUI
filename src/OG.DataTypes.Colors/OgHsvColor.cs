namespace OG.DataTypes.Color;

public struct OgHsvColor(float h, float s, float v)
{
    public float H { get; set; } = h;

    public float S { get; set; } = s;

    public float V { get; set; } = v;
}
