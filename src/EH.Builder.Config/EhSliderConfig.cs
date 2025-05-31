using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Config;
public class EhSliderConfig
{
    public float             Height                 { get; set; } = 5;
    public float             Width                  { get; set; } = 100;
    public float             ThumbOutlineSize       { get; set; } = 16;
    public float             ThumbSize              { get; set; } = 12;
    public float             ThumbBorder            { get; set; } = 90f;
    public float             BackgroundBorder       { get; set; } = 90f;
    public int               NameTextFontSize       { get; set; } = 14;
    public int               ValueTextFontSize      { get; set; } = 10;
    public TextAnchor        NameTextAlignment      { get; set; } = TextAnchor.MiddleLeft;
    public TextAnchor        ValueTextAlignment     { get; set; } = TextAnchor.UpperRight;
    public DkProperty<Color> BackgroundColor        { get; }      = new(Color.black);
    public DkProperty<Color> BackgroundHoverColor   { get; }      = new(Color.black);
    public DkProperty<Color> FillColor              { get; }      = new(Color.white);
    public DkProperty<Color> FillHoverColor         { get; }      = new(new(0.8f, 0.8f, 0.8f, 1f));
    public DkProperty<Color> TextColor              { get; }      = new(Color.white);
    public DkProperty<Color> ThumbColor             { get; }      = new(Color.black);
    public DkProperty<Color> ThumbHoverColor        { get; }      = new(new(0.2f, 0.2f, 0.2f, 1f));
    public DkProperty<Color> ThumbOutlineColor      { get; }      = new(Color.white);
    public DkProperty<Color> ThumbOutlineHoverColor { get; }      = new(new(0.8f, 0.8f, 0.8f, 1f));
}