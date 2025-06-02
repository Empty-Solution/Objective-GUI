using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement<OgTextGraphicsContext>(name, provider, rectGetter), IOgTextElement
{
    public IDkGetProvider<Color>?      ColorProvider { get; set; }
    public IDkGetProvider<TextAnchor>? Alignment     { get; set; }
    public Font?                       Font          { get; set; }
    public FontStyle                   FontStyle     { get; set; }
    public IDkGetProvider<int>?        FontSize      { get; set; }
    public TextClipping                TextClipping  { get; set; }
    public bool                        WordWrap      { get; set; }
    public IDkGetProvider<string>?     Text          { get; set; }
    public IOgTextGraphicsContext?     Context       { get; private set; }
    protected override void FillContext()
    {
        if(Font is null) return;
        if(m_RenderContext is null) Context = m_RenderContext = new(Text?.Get() ?? string.Empty);
        m_RenderContext.Color        = ColorProvider!.Get();
        m_RenderContext.Alignment    = Alignment?.Get() ?? TextAnchor.UpperLeft;
        m_RenderContext.TextClipping = TextClipping;
        m_RenderContext.Font         = Font;
        m_RenderContext.FontSize     = FontSize?.Get() ?? 12;
        m_RenderContext.FontStyle    = FontStyle;
        m_RenderContext.Text         = Text?.Get() ?? string.Empty;
        m_RenderContext.WordWrap     = WordWrap;
    }
}