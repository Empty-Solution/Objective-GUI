using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics;
using OG.Graphics.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement<OgTextGraphicsContext>(name, provider, rectGetter), IOgTextElement
{
    public TextAnchor              Alignment    { get; set; }
    public Font?                   Font         { get; set; }
    public FontStyle               FontStyle    { get; set; }
    public int                     FontSize     { get; set; }
    public Color                   Color        { get; set; }
    public TextClipping            TextClipping { get; set; }
    public bool                    WordWrap     { get; set; }
    public string                  Text         { get; set; } = string.Empty;
    public IOgTextGraphicsContext? Context      { get; private set; }
    protected override void FillContext()
    {
        if(Font is null) return;
        if(m_RenderContext is null) Context = m_RenderContext = new(Text);
        m_RenderContext.Alignment    = Alignment;
        m_RenderContext.TextClipping = TextClipping;
        m_RenderContext.Color        = Color;
        m_RenderContext.Font         = Font;
        m_RenderContext.FontSize     = FontSize;
        m_RenderContext.FontStyle    = FontStyle;
        m_RenderContext.Text         = Text;
        m_RenderContext.WordWrap     = WordWrap;
    }
}