using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Color;
using OG.DataTypes.Sprite;
using OG.DataTypes.Vector.Float;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics;
namespace OG.Element.Visual;
public class OgTexture(IOgEventProvider eventProvider) : OgVisualElement<IOgRepaintEvent, bool>(eventProvider), IOgTexture
{
    private readonly OgTextureRepaintContext      m_Context = new();
    public           IDkGetProvider<float>?       AlphaBlend  { get; set; }
    public           IDkGetProvider<OgVector4F>?  Widths      { get; set; }
    public           IDkGetProvider<OgVector4F>?  Radiuses    { get; set; }
    public           IDkGetProvider<float>?       ImageAspect { get; set; }
    public           IDkGetProvider<OgSprite>?    Sprite      { get; set; }
    public           IDkGetProvider<OgRgbaColor>? Color       { get; set; }
    public override bool HandleRepaint(IOgRepaintEvent reason)
    {
        m_Context.Color       = Color?.Get() ?? new(1, 1, 1, 1);
        m_Context.AlphaBlend  = AlphaBlend?.Get() ?? 1;
        m_Context.Widths      = Widths?.Get() ?? new();
        m_Context.Radiuses    = Radiuses?.Get() ?? new();
        m_Context.ImageAspect = ImageAspect?.Get() ?? 1;
        m_Context.Sprite      = Sprite?.Get() ?? new();
        return true;
    }
}