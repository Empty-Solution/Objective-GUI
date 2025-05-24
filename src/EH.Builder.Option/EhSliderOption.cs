using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhSliderOption
{
    private readonly Color m_BackgroundColor     = Color.black;
    private readonly Color m_BackgroundFillColor = Color.white;
    private readonly Color m_TextColor           = Color.white;
    private readonly Color m_ThumbColor          = Color.black;
    private readonly Color m_ThumbOutlineColor   = Color.white;
    public EhSliderOption()
    {
        BackgroundColorProperty     = new(m_BackgroundColor);
        BackgroundFillColorProperty = new(m_BackgroundFillColor);
        TextColorProperty           = new(m_TextColor);
        ThumbColorProperty          = new(m_ThumbColor);
        ThumbOutlineColorProperty   = new(m_ThumbOutlineColor);
    }
    public float             BackgroundBorder            { get; set; } = 90f;
    public int               NameFontSize                { get; set; } = 14;
    public int               ValueFontSize               { get; set; } = 10;
    public float             SliderHeight                { get; set; } = 5;
    public float             SliderThumbOutlineSize      { get; set; } = 16;
    public float             SliderThumbSize             { get; set; } = 12;
    public float             SliderWidth                 { get; set; } = 150;
    public float             ThumbBorder                 { get; set; } = 90f;
    public TextAnchor        NameAlignment               { get; set; } = TextAnchor.MiddleLeft;
    public TextAnchor        ValueAlignment              { get; set; } = TextAnchor.UpperRight;
    public DkProperty<Color> BackgroundColorProperty     { get; }
    public DkProperty<Color> BackgroundFillColorProperty { get; }
    public DkProperty<Color> TextColorProperty           { get; }
    public DkProperty<Color> ThumbColorProperty          { get; }
    public DkProperty<Color> ThumbOutlineColorProperty   { get; }
}