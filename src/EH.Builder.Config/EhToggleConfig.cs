using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Config;
public class EhToggleConfig
{
    public float             Height               { get; set; } = 22;
    public float             Width                { get; set; } = 44;
    public float             ThumbSize            { get; set; } = 16;
    public float             ThumbBorder          { get; set; } = 90f;
    public float             BackgroundBorder     { get; set; } = 90f;
    public int               NameTextFontSize     { get; set; } = 14;
    public TextAnchor        NameTextAlignment    { get; set; } = TextAnchor.MiddleLeft;
    public DkProperty<Color> BackgroundColor      { get; }      = new(new Color32(10, 10, 10, 255));
    public DkProperty<Color> BackgroundHoverColor { get; }      = new(new Color32(20, 20, 20, 255));
    public DkProperty<Color> FillColor            { get; }      = new(Color.white);
    public DkProperty<Color> FillHoverColor       { get; }      = new(new(0.8f, 0.8f, 0.8f, 1f));
    public DkProperty<Color> TextColor            { get; }      = new(Color.white);
    public DkProperty<Color> ThumbColor           { get; }      = new(Color.black);
    public DkProperty<Color> ThumbHoverColor      { get; }      = new(new(0.2f, 0.2f, 0.2f, 1f));
}