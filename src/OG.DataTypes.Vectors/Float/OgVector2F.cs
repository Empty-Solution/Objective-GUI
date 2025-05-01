namespace OG.DataTypes.Vector.Float;

public struct OgVector2F(float x, float y)
{
    public float X { get; set; } = x;

    public float Y { get; set; } = y;

    public static OgVector2F operator +(OgVector2F left, OgVector2F right) => new(left.X + right.X, left.Y + right.Y);

    public static OgVector2F operator -(OgVector2F left, OgVector2F right) => new(left.X - right.X, left.Y - right.Y);

    public readonly bool Equals(OgVector2F other) => X == other.X && Y == other.Y;
}