namespace OG.DataTypes.Color.Bit;

public struct OgHsvColor32(byte h, byte s, byte v)
{
    public byte H { get; set; } = h;

    public byte S { get; set; } = s;

    public byte V { get; set; } = v;
}
