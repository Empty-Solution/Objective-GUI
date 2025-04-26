namespace OG.DataTypes.Colors.Bits;

public struct OgRgbColor32(byte r, byte g, byte b)
{
    public byte R { get; set; } = r;

    public byte G { get; set; } = g;

    public byte B { get; set; } = b;
}
