using DK.Property.Abstraction.Generic;
using OG.DataTypes.Sprite;
using OG.DataTypes.Vector.Float;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction;

namespace OG.Element.Visual;

public class OgTexture(IOgEventProvider eventProvider) : OgVisualElement<IOgRepaintEvent, bool>(eventProvider), IOgTexture
{
    private readonly OgTextureRepaintContext  m_Context = new();
    public           IDkProperty<float>?      AlphaBlend  { get; set; }
    public           IDkProperty<OgVector4F>? Widths      { get; set; }
    public           IDkProperty<OgVector4F>? Radiuses    { get; set; }
    public           IDkProperty<float>?      ImageAspect { get; set; }
    public           IDkProperty<OgSprite>?   Sprite      { get; set; }

    public override bool HandleRepaint(IOgRepaintEvent reason)
    {
        m_Context.AlphaBlend  = AlphaBlend!.Get();
        m_Context.Widths      = Widths!.Get();
        m_Context.Radiuses    = Radiuses!.Get();
        m_Context.ImageAspect = ImageAspect!.Get();
        m_Context.Sprite      = Sprite!.Get();
        return true;
    }
}