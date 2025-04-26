namespace OG.DataTypes.Color.Bit;

public struct OgHsvaColor32(byte h, byte s, byte v, byte a)
{
    public byte H { get; set; } = h;

    public byte S { get; set; } = s;

    public byte V { get; set; } = v;

    public byte A { get; set; } = a;
}
