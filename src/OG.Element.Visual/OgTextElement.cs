using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement(name, provider, rectGetter), IOgTextElement
{
    private Font?     m_Font;
    private int       m_FontSize      = 14;
    private FontStyle m_FontStyle     = FontStyle.Normal;
    private float     m_PixelsPerUnit = 1f;
    private float     m_Spacing       = 1f;
    private string    m_Text          = string.Empty;
    public float Spacing
    {
        get => m_Spacing;
        set
        {
            if(Mathf.Approximately(m_Spacing, value)) return;
            m_Spacing = value;
            MarkDirty();
        }
    }
    public Font Font
    {
        get => m_Font!;
        set
        {
            if(m_Font == value) return;
            m_Font = value;
            MarkDirty();
        }
    }
    public FontStyle FontStyle
    {
        get => m_FontStyle;
        set
        {
            if(m_FontStyle == value) return;
            m_FontStyle = value;
            MarkDirty();
        }
    }
    public int FontSize
    {
        get => m_FontSize;
        set
        {
            if(m_FontSize == value) return;
            m_FontSize = value;
            MarkDirty();
        }
    }
    public float PixelsPerUnit
    {
        get => m_PixelsPerUnit;
        set
        {
            if(Mathf.Approximately(m_PixelsPerUnit, value)) return;
            m_PixelsPerUnit = value;
            MarkDirty();
        }
    }
    public override Color Color
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            MarkDirty();
        }
    }
    public string Text
    {
        get => m_Text;
        set
        {
            if(m_Text == value) return;
            m_Text = value;
            MarkDirty();
        }
    }
    protected override void BuildContext(OgGraphicsContext context)
    {
        RegenerateTextMesh(context);
        context.Material = m_Font!.material;
        context.Rect = ElementRect.Get();
    }
    private void RegenerateTextMesh(OgGraphicsContext context)
    {
        if(string.IsNullOrEmpty(m_Text) || m_Font is null) return;
        m_Font.RequestCharactersInTexture(m_Text, m_FontSize, m_FontStyle);
        float x          = 0f;
        float y          = 0f;
        float lineHeight = m_Font.lineHeight * m_FontSize / m_PixelsPerUnit * (m_Spacing * 0.05f);
        float spaceWidth = m_Font.GetCharacterInfo(' ', out CharacterInfo spaceInfo, m_FontSize, m_FontStyle) ? spaceInfo.advance / m_PixelsPerUnit
                               : m_FontSize * 0.5f;
        int   vertexIndex = context.IndicesCount;
        Color color       = Color;
        foreach(char c in m_Text)
        {
            switch(c)
            {
                case '\n':
                    x =  0f;
                    y -= lineHeight;
                    continue;
                case ' ':
                    x += spaceWidth;
                    continue;
            }
            if(!m_Font.GetCharacterInfo(c, out CharacterInfo charInfo, m_FontSize, m_FontStyle)) continue;
            Vector3 bottomLeft = new(x + (charInfo.minX / m_PixelsPerUnit), -(y + (charInfo.minY / m_PixelsPerUnit)), 0);
            Vector3 topRight   = new(x + (charInfo.maxX / m_PixelsPerUnit), -(y + (charInfo.maxY / m_PixelsPerUnit)), 0);
            context.AddVertex(new(new(bottomLeft.x, bottomLeft.y, 0), color, new(charInfo.uvBottomLeft.x, charInfo.uvBottomLeft.y)));
            context.AddVertex(new(new(bottomLeft.x, topRight.y, 0), color, new(charInfo.uvTopLeft.x, charInfo.uvTopLeft.y)));
            context.AddVertex(new(new(topRight.x, topRight.y, 0), color, new(charInfo.uvTopRight.x, charInfo.uvTopRight.y)));
            context.AddVertex(new(new(topRight.x, bottomLeft.y, 0), color, new(charInfo.uvBottomRight.x, charInfo.uvBottomRight.y)));
            context.AddIndex(vertexIndex);
            context.AddIndex(vertexIndex + 1);
            context.AddIndex(vertexIndex + 2);
            context.AddIndex(vertexIndex);
            context.AddIndex(vertexIndex + 2);
            context.AddIndex(vertexIndex + 3);
            vertexIndex += 4;
            x           += charInfo.advance / m_PixelsPerUnit;
        }
    }
}