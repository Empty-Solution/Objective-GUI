using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhToggleOption
{
    public EhToggleOption()
    {
        BackgroundColor      = new(new Color32(30, 30, 30, 255));
        FillColor            = new(Color.white);
        TextColor            = new(Color.white);
        ThumbColor           = new(Color.black);
        FillHoverColor       = new(new(0.8f, 0.8f, 0.8f, 1f));
        ThumbHoverColor      = new(new(0.2f, 0.2f, 0.2f, 1f));
        BackgroundHoverColor = new(new Color32(30, 30, 30, 255));
    }
    public float             BackgroundBorder { get; set; } = 90f;
    public int               FontSize         { get; set; } = 14;
    public float             ToggleHeight     { get; set; } = 22;
    public float             ToggleWidth      { get; set; } = 44;
    public float             ThumbSize        { get; set; } = 16;
    public float             ThumbBorder      { get; set; } = 90f;
    public TextAnchor        NameAlignment    { get; set; } = TextAnchor.MiddleLeft;
    public DkProperty<Color> BackgroundColor  { get; }
    public DkProperty<Color> BackgroundHoverColor  { get; }
    public DkProperty<Color> FillColor      { get; }
    public DkProperty<Color> FillHoverColor { get; }
    public DkProperty<Color> TextColor                { get; }
    public DkProperty<Color> ThumbColor               { get; }
    public DkProperty<Color> ThumbHoverColor               { get; }
}