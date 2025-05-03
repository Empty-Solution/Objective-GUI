namespace OG.DataTypes.Point;
public struct OgPoint(int x, int y)
{
    public        int X                                           { get; set; } = x;
    public        int Y                                           { get; set; } = y;
    public static OgPoint operator +(OgPoint left, OgPoint right) => new(left.X + right.X, left.Y + right.Y);
    public static OgPoint operator -(OgPoint left, OgPoint right) => new(left.X - right.X, left.Y - right.Y);
    public readonly bool Equals(OgPoint          other) => (X == other.X) && (Y == other.Y);
}