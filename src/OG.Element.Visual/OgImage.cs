// TODO: Переработать элемент. Поведение было привязано к системе ивентов
/*
using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Color;
using OG.DataTypes.ScaleMode;
using OG.DataTypes.Vector.Float;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Element.Visual;
public class OgImage(IOgEventProvider eventProvider) : OgVisualElement<IOgRepaintEvent, bool>(eventProvider), IOgImage
{
    private readonly OgTextureRepaintContext       m_Context = new();
    public           IDkGetProvider<bool>?         AlphaBlend  { get; set; }
    public           IDkGetProvider<OgVector4F>?   Widths      { get; set; }
    public           IDkGetProvider<OgVector4F>?   Radiuses    { get; set; }
    public           IDkGetProvider<EOgScaleMode>? ScaleMode   { get; set; }
    public           IDkGetProvider<float>?        ImageAspect { get; set; }
    public           IDkGetProvider<OgRgbaColor>?  Color       { get; set; }
    public override bool HandleRepaint(IOgRepaintEvent reason)
    {
        m_Context.AlphaBlend  = AlphaBlend?.Get() ?? true;
        m_Context.Widths      = Widths?.Get() ?? new();
        m_Context.Radiuses    = Radiuses?.Get() ?? new();
        m_Context.ScaleMode   = ScaleMode?.Get() ?? EOgScaleMode.STRETCH_TO_FILL;
        m_Context.ImageAspect = ImageAspect?.Get() ?? 1;
        m_Context.Color       = Color?.Get() ?? new(1, 1, 1, 1);
        return true;
    }
}
*/
