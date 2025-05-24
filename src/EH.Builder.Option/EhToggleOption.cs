using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhToggleOption
{
    private readonly Color m_BackgroundColor          = new Color32(30, 30, 30, 255);
    private readonly Color m_BackgroundFillColor      = Color.white;
    private readonly Color m_BackgroundFillHoverColor = new(0.8f, 0.8f, 0.8f, 1f);
    private readonly Color m_TextColor                = Color.white;
    private readonly Color m_ThumbColor               = Color.black;
    private readonly Color m_ThumbHoverColor               = new(0.2f, 0.2f, 0.2f, 1f);
    public EhToggleOption()
    {
        BackgroundColor          = new(m_BackgroundColor);
        BackgroundFillColor      = new(m_BackgroundFillColor);
        TextColor                = new(m_TextColor);
        ThumbColor               = new(m_ThumbColor);
        BackgroundFillHoverColor = new(m_BackgroundFillHoverColor);
        ThumbHoverColor          = new(m_ThumbHoverColor);
    }
    public float             BackgroundBorder                 { get; set; } = 90f;
    public int               FontSize                         { get; set; } = 14;
    public float             ToggleHeight                     { get; set; } = 22;
    public float             ToggleWidth                      { get; set; } = 44;
    public float             ThumbSize                        { get; set; } = 16;
    public float             ThumbBorder                      { get; set; } = 90f;
    public TextAnchor        NameAlignment                    { get; set; } = TextAnchor.MiddleLeft;
    public DkProperty<Color> BackgroundColor          { get; }
    public DkProperty<Color> BackgroundFillColor      { get; }
    public DkProperty<Color> BackgroundFillHoverColor { get; }
    public DkProperty<Color> TextColor                { get; }
    public DkProperty<Color> ThumbColor               { get; }
    public DkProperty<Color> ThumbHoverColor               { get; }
}