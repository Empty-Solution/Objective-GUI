using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhSliderOption
{
    private readonly Color m_BackgroundColor        = Color.black;
    private readonly Color m_FillColor              = Color.white;
    private readonly Color m_FillHoverColor         = new(0.8f, 0.8f, 0.8f, 1f);
    private readonly Color m_TextColor              = Color.white;
    private readonly Color m_ThumbColor             = Color.black;
    private readonly Color m_ThumbHoverColor        = new(0.2f, 0.2f, 0.2f, 1f);
    private readonly Color m_ThumbOutlineColor      = Color.white;
    private readonly Color m_ThumbOutlineHoverColor = new(0.8f, 0.8f, 0.8f, 1f);
    public EhSliderOption()
    {
        BackgroundColor        = new(m_BackgroundColor);
        FillColor              = new(m_FillColor);
        TextColor              = new(m_TextColor);
        ThumbColor             = new(m_ThumbColor);
        ThumbOutlineColor      = new(m_ThumbOutlineColor);
        ThumbOutlineHoverColor = new(m_ThumbOutlineHoverColor);
        ThumbHoverColor        = new(m_ThumbHoverColor);
        FillHoverColor         = new(m_FillHoverColor);
    }
    public float             BackgroundBorder       { get; set; } = 90f;
    public int               NameFontSize           { get; set; } = 14;
    public int               ValueFontSize          { get; set; } = 10;
    public float             SliderHeight           { get; set; } = 5;
    public float             SliderThumbOutlineSize { get; set; } = 16;
    public float             SliderThumbSize        { get; set; } = 12;
    public float             SliderWidth            { get; set; } = 150;
    public float             ThumbBorder            { get; set; } = 90f;
    public TextAnchor        NameAlignment          { get; set; } = TextAnchor.MiddleLeft;
    public TextAnchor        ValueAlignment         { get; set; } = TextAnchor.UpperRight;
    public DkProperty<Color> BackgroundColor        { get; }
    public DkProperty<Color> FillColor              { get; }
    public DkProperty<Color> FillHoverColor              { get; }
    public DkProperty<Color> TextColor              { get; }
    public DkProperty<Color> ThumbColor             { get; }
    public DkProperty<Color> ThumbHoverColor        { get; }
    public DkProperty<Color> ThumbOutlineColor      { get; }
    public DkProperty<Color> ThumbOutlineHoverColor      { get; }
}