using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Color;
using OG.DataTypes.ElementAlignment;
using OG.DataTypes.Font.Abstraction;
using OG.DataTypes.FontStyle;
using OG.DataTypes.TextClipping;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Element.Visual;
public class OgText(IOgEventProvider eventProvider) : OgVisualElement<IOgTextRepaintEvent, OgTextRepaintContext>(eventProvider), IOgText
{
    private readonly OgTextRepaintContext                 m_Context = new();
    public           IDkGetProvider<int>?                 FontSize  { get; set; }
    public           IDkGetProvider<string>?              Text      { get; set; }
    public           IDkGetProvider<bool>?                WordWrap  { get; set; }
    public           IDkGetProvider<EOgFontStyle>?        FontStyle { get; set; }
    public           IDkGetProvider<EOgElementAlignment>? Alignment { get; set; }
    public           IDkGetProvider<EOgTextClipping>?     Clipping  { get; set; }
    public           IDkGetProvider<IOgFont>?             Font      { get; set; }
    public           IDkGetProvider<OgRgbaColor>?         Color     { get; set; }
    public override OgTextRepaintContext HandleRepaint(IOgTextRepaintEvent reason)
    {
        m_Context.Color     = Color?.Get() ?? new(1, 1, 1, 1);
        m_Context.FontSize  = FontSize?.Get() ?? 14;
        m_Context.Text      = Text?.Get() ?? string.Empty;
        m_Context.WordWrap  = WordWrap?.Get() ?? true;
        m_Context.FontStyle = FontStyle?.Get() ?? EOgFontStyle.NORMAL;
        m_Context.Alignment = Alignment?.Get() ?? EOgElementAlignment.UPPER_LEFT;
        m_Context.Clipping  = Clipping?.Get() ?? EOgTextClipping.OVERFLOW;
        m_Context.Font      = Font!.Get();
        reason.GraphicsTool.Repaint(m_Context);
        return m_Context;
    }
}