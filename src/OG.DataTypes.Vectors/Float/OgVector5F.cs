namespace OG.DataTypes.Vector.Float;

public struct OgVector5F(float x, float y, float z, float w, float h)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;

    public float Z { get; set; } = z;

    public float W { get; set; } = w;

    public float H { get; set; } = h;
}