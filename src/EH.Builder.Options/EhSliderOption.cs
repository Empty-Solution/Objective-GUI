using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhSliderOption
{
    public EhSliderOption()
    {
        BackgroundColor        = new(Color.black);
        FillColor              = new(Color.white);
        TextColor              = new(Color.white);
        ThumbColor             = new(Color.black);
        ThumbOutlineColor      = new(Color.white);
        ThumbOutlineHoverColor = new(new(0.8f, 0.8f, 0.8f, 1f));
        ThumbHoverColor        = new(new(0.2f, 0.2f, 0.2f, 1f));
        FillHoverColor         = new(new(0.8f, 0.8f, 0.8f, 1f));
        BackgroundHoverColor   = new(Color.black);
    }
    public int               NameFontSize           { get; set; } = 14;
    public int               ValueFontSize          { get; set; } = 10;
    public float             Height                 { get; set; } = 5;
    public float             ThumbOutlineSize       { get; set; } = 16;
    public float             ThumbSize              { get; set; } = 12;
    public float             Width                  { get; set; } = 100;
    public float             ThumbBorder            { get; set; } = 90f;
    public float             BackgroundBorder       { get; set; } = 90f;
    public TextAnchor        NameAlignment          { get; set; } = TextAnchor.MiddleLeft;
    public TextAnchor        ValueAlignment         { get; set; } = TextAnchor.UpperRight;
    public DkProperty<Color> BackgroundColor        { get; }
    public DkProperty<Color> BackgroundHoverColor   { get; }
    public DkProperty<Color> FillColor              { get; }
    public DkProperty<Color> FillHoverColor         { get; }
    public DkProperty<Color> TextColor              { get; }
    public DkProperty<Color> ThumbColor             { get; }
    public DkProperty<Color> ThumbHoverColor        { get; }
    public DkProperty<Color> ThumbOutlineColor      { get; }
    public DkProperty<Color> ThumbOutlineHoverColor { get; }
}