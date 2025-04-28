namespace OG.DataTypes.Vector.Float;

public struct OgVector4F(float x, float y, float z, float w)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;

    public float Z { get; set; } = z;

    public float W { get; set; } = w;
}