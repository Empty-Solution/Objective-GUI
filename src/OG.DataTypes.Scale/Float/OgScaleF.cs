namespace OG.DataTypes.Scale.Float;

public struct OgScaleF(float x, float y)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;
}
