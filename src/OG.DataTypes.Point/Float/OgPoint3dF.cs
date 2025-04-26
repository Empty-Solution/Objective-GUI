namespace OG.DataTypes.Point.Float;

public struct OgPoint3dF(float x, float y, float z)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;

    public float Z { get; set; } = z;
}
