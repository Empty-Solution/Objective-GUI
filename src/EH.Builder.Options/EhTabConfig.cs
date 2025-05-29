using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhTabConfig
{
    public TextAnchor        GroupTitleAlignment = TextAnchor.MiddleLeft;
    public int               GroupTitleFontSize  = 11;
    public float             GroupTitleHeight    = 16;
    public DkProperty<Color> BackgroundColor     { get; }      = new(new Color32(25, 25, 25, 255));
    public float             BackgroundBorder    { get; set; } = 5;
    public float             TabContainerWidth   { get; set; } = 270;
    public float             TabContainerPadding { get; set; } = 25;
    public DkProperty<Color> GroupTitleColor     { get; }      = new(new Color32(255, 255, 255, 255));
}