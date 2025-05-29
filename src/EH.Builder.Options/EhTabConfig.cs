using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhTabConfig
{
    public TextAnchor        TabTitleAlignment = TextAnchor.UpperLeft;
    public int               TabTitleFontSize  = 14;
    public DkProperty<Color> BackgroundColor     { get; }      = new(new Color32(25, 25, 25, 255));
    public float             BackgroundBorder    { get; set; } = 5;
    public float             TabContainerWidth   { get; set; } = 270;
    public float             TabContainerPadding { get; set; } = 25;
}