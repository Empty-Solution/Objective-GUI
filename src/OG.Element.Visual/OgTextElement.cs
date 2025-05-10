using OG.Event.Abstraction;
using OG.Graphics;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextElement(string name, IOgEventHandlerProvider provider) : OgVisualElement(name, provider)
{
    private TextAnchor m_Alignment     = TextAnchor.UpperLeft;
    private Font?      m_Font;
    private int        m_FontSize      = 14;
    private FontStyle  m_FontStyle     = FontStyle.Normal;
    private float      m_PixelsPerUnit = 1f;
    private string     m_Text          = string.Empty;
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
    public TextAnchor Alignment
    {
        get => m_Alignment;
        set
        {
            if(m_Alignment == value) return;
            m_Alignment = value;
            MarkDirty();
        }
    }
    public bool RichText
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            MarkDirty();
        }
    } = false;
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
    protected override void BuildContext(OgGraphicsContext context)
    {
        RegenerateTextMesh(context);
        context.Material = m_Font!.material;
        // context.Texture  = m_Font.material.mainTexture;
        context.Rect = ElementRect;
    }
    private void RegenerateTextMesh(OgGraphicsContext context)
    {
        if(string.IsNullOrEmpty(m_Text) || m_Font == null) return;
        m_Font.RequestCharactersInTexture(m_Text, m_FontSize, m_FontStyle);
        float x          = 0f;
        float y          = 0f;
        float lineHeight = m_Font.lineHeight * m_FontSize / m_PixelsPerUnit;
        float spaceWidth = m_Font.GetCharacterInfo(' ', out CharacterInfo spaceInfo, m_FontSize, m_FontStyle) ? spaceInfo.advance / m_PixelsPerUnit
                               : m_FontSize * 0.5f;

        //Vector2 textSize = CalculateTextSize(m_Text, m_Font, m_FontSize, m_PixelsPerUnit, spaceWidth);
        Vector2 alignmentOffset = Vector2.zero; //GetAlignmentOffset(textSize, context.Rect);
        int     vertexIndex     = context.IndicesCount;
        Color   color           = Color;
        foreach(char c in m_Text)
        {
            if(c == '\n')
            {
                x =  0f;
                y -= lineHeight;
                continue;
            }
            if(!m_Font.GetCharacterInfo(c, out CharacterInfo charInfo, m_FontSize, m_FontStyle)) continue;
            if(c == ' ')
            {
                x += spaceWidth;
                continue;
            }
            Vector3 bottomLeft = new(x + (charInfo.minX / m_PixelsPerUnit) + alignmentOffset.x,
                                     -(y + (charInfo.minY / m_PixelsPerUnit)) + alignmentOffset.y, 0);
            Vector3 topRight = new(x + (charInfo.maxX / m_PixelsPerUnit) + alignmentOffset.x, -(y + (charInfo.maxY / m_PixelsPerUnit)) + alignmentOffset.y,
                                   0);
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
    private Vector2 GetAlignmentOffset(Vector2 textSize, Rect rect) =>
        new(m_Alignment switch
        {
            TextAnchor.UpperLeft or TextAnchor.MiddleLeft or TextAnchor.LowerLeft       => 0f,
            TextAnchor.UpperCenter or TextAnchor.MiddleCenter or TextAnchor.LowerCenter => (rect.width - textSize.x) * 0.5f,
            TextAnchor.UpperRight or TextAnchor.MiddleRight or TextAnchor.LowerRight    => rect.width - textSize.x,
            _                                                                           => 0f
        }, m_Alignment switch
        {
            TextAnchor.UpperLeft or TextAnchor.UpperCenter or TextAnchor.UpperRight    => 0f,
            TextAnchor.MiddleLeft or TextAnchor.MiddleCenter or TextAnchor.MiddleRight => (rect.height - textSize.y) * 0.5f,
            TextAnchor.LowerLeft or TextAnchor.LowerCenter or TextAnchor.LowerRight    => rect.height - textSize.y,
            _                                                                          => 0f
        });
    private Vector2 CalculateTextSize(
        string text, Font font, int fontSize, float pixelsPerUnit,
        float spaceWidth)
    {
        float width     = 0f;
        float maxWidth  = 0f;
        int   lineCount = 1;
        foreach(char c in text)
        {
            switch(c)
            {
                case '\n':
                    maxWidth = Mathf.Max(maxWidth, width);
                    width    = 0f;
                    lineCount++;
                    continue;
                case ' ':
                    width += spaceWidth;
                    continue;
            }
            if(font.GetCharacterInfo(c, out CharacterInfo charInfo, fontSize, m_FontStyle)) width += charInfo.advance / pixelsPerUnit;
        }
        maxWidth = Mathf.Max(maxWidth, width);
        float height = lineCount * font.lineHeight * fontSize / pixelsPerUnit;
        return new(maxWidth, height);
    }
}