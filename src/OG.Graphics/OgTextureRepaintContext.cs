using OG.DataTypes.Color;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Sprite;
using OG.DataTypes.Vector.Float;
using OG.Graphics.Abstraction;
namespace OG.Graphics;
public class OgTextureRepaintContext : IOgRepaintContext
{
    public float       AlphaBlend  { get; set; }
    public OgVector4F  Widths      { get; set; }
    public OgVector4F  Radiuses    { get; set; }
    public float       ImageAspect { get; set; }
    public OgSprite    Sprite      { get; set; }
    public OgRgbaColor Color       { get; set; }
    public OgRectangle RepaintRect { get; set; }
}