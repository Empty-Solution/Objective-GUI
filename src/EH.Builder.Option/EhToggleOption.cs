using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhToggleOption
{
    private readonly Color m_BackgroundColor     = new Color32(30, 30, 30, 255);
    private readonly Color m_BackgroundFillColor = Color.white;
    private readonly Color m_TextColor           = Color.white;
    private readonly Color m_ThumbColor          = Color.black;
    public EhToggleOption()
    {
        BackgroundColorProperty     = new(m_BackgroundColor);
        BackgroundFillColorProperty = new(m_BackgroundFillColor);
        TextColorProperty           = new(m_TextColor);
        ThumbColorProperty          = new(m_ThumbColor);
    }
    public float             BackgroundBorder            { get; set; } = 90f;
    public int               FontSize                    { get; set; } = 14;
    public float             ToggleHeight                { get; set; } = 22;
    public float             ToggleWidth                 { get; set; } = 44;
    public float             ThumbSize                   { get; set; } = 16;
    public float             ThumbBorder                 { get; set; } = 90f;
    public TextAnchor        NameAlignment               { get; set; } = TextAnchor.MiddleLeft;
    public DkProperty<Color> BackgroundColorProperty     { get; }
    public DkProperty<Color> BackgroundFillColorProperty { get; }
    public DkProperty<Color> TextColorProperty           { get; }
    public DkProperty<Color> ThumbColorProperty          { get; }
}