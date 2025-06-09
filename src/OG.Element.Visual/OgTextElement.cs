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
    private static readonly GUIStyle? tempStyle = new()
    {
        normal = new()
    };
    private static readonly GUIContent                  tempContent = new();
    public                  IDkGetProvider<Color>?      ColorProvider { get; set; }
    public                  IDkGetProvider<TextAnchor>? Alignment     { get; set; }
    public                  IDkGetProvider<Font?>?      Font          { get; set; }
    public                  FontStyle                   FontStyle     { get; set; }
    public                  IDkGetProvider<int>?        FontSize      { get; set; }
    public                  TextClipping                TextClipping  { get; set; }
    public                  bool                        WordWrap      { get; set; }
    public                  IDkGetProvider<float>?      OutlineSize   { get; set; }
    public                  IDkGetProvider<Color>?      OutlineColor  { get; set; }
    public                  IDkGetProvider<string>?     Text          { get; set; }
    public                  IOgTextGraphicsContext?     Context       { get; private set; }
    public Vector2 CalculateSize()
    {
        if(Font is null || string.IsNullOrEmpty(Text?.Get())) return Vector2.zero;
        tempContent.text    = Text?.Get() ?? string.Empty;
        tempStyle!.fontSize = FontSize?.Get() ?? 12;
        tempStyle.alignment = Alignment?.Get() ?? TextAnchor.UpperLeft;
        tempStyle.fontStyle = FontStyle;
        tempStyle.clipping  = TextClipping;
        tempStyle.wordWrap  = WordWrap;
        tempStyle.font      = Font?.Get();
        return tempStyle.CalcSize(tempContent);
    }
    protected override void FillContext()
    {
        if(m_RenderContext is null) Context = m_RenderContext = new(Text?.Get() ?? string.Empty);
        m_RenderContext.Color        = ColorProvider?.Get() ?? Color.white;
        m_RenderContext.Alignment    = Alignment?.Get() ?? TextAnchor.UpperLeft;
        m_RenderContext.TextClipping = TextClipping;
        m_RenderContext.Font         = Font?.Get();
        m_RenderContext.FontSize     = FontSize?.Get() ?? 12;
        m_RenderContext.FontStyle    = FontStyle;
        m_RenderContext.Text         = Text?.Get() ?? string.Empty;
        m_RenderContext.WordWrap     = WordWrap;
        m_RenderContext.OutlineSize  = OutlineSize?.Get() ?? 0;
        m_RenderContext.OutlineColor = OutlineColor?.Get() ?? Color.black;
    }
}