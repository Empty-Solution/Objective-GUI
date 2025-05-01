using DK.Property.Abstraction.Generic;
using OG.DataTypes.Font;
using OG.DataTypes.FontStyle;
using OG.DataTypes.TextAnchor;
using OG.DataTypes.TextClipping;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
using OG.Graphics.Abstraction;

namespace OG.Element.Visual;

public class OgText(IOgEventProvider eventProvider) : OgVisualElement<IOgTextRepaintEvent, OgTextRepaintContext>(eventProvider), IOgText
{
    private readonly OgTextRepaintContext          m_Context = new();
    public           IDkProperty<int>?             FontSize  { get; set; }
    public           IDkProperty<string>?          Text      { get; set; }
    public           IDkProperty<bool>?            WordWrap  { get; set; }
    public           IDkProperty<EOgFontStyle>?    FontStyle { get; set; }
    public           IDkProperty<EOgTextAnchor>?   Alignment { get; set; }
    public           IDkProperty<EOgTextClipping>? Clipping  { get; set; }
    public           IDkProperty<OgFont>?          Font      { get; set; }

    public override OgTextRepaintContext HandleRepaint(IOgTextRepaintEvent reason)
    {
        m_Context.FontSize  = FontSize!.Get();
        m_Context.Text      = Text!.Get();
        m_Context.WordWrap  = WordWrap!.Get();
        m_Context.FontStyle = FontStyle!.Get();
        m_Context.Alignment = Alignment!.Get();
        m_Context.Clipping  = Clipping!.Get();
        m_Context.Font      = Font!.Get();
        reason.GraphicsTool.Repaint(m_Context);
        return m_Context;
    }
}