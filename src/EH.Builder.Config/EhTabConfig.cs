using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Config;
public class EhTabConfig
{
    public float             Width               { get; set; } = 270;
    public float             BackgroundBorder    { get; set; } = 5;
    public float             TabContainerPadding { get; set; } = 25;
    public float             GroupTitleHeight    { get; set; } = 16;
    public TextAnchor        GroupTitleAlignment { get; set; } = TextAnchor.MiddleLeft;
    public int               GroupTitleFontSize  { get; set; } = 11;
    public DkProperty<Color> BackgroundColor     { get; }      = new(new Color32(25, 25, 25, 255));
    public DkProperty<Color> GroupTitleColor     { get; }      = new(new Color32(255, 255, 255, 255));
}