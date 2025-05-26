using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhTabButtonOption
{
    public DkProperty<Color> ButtonColor         { get; }      = new(new Color32(150, 150, 150, 150));
    public DkProperty<Color> InteractColor       { get; }      = new(Color.white);
    public DkProperty<Color> BackgroundColor     { get; }      = new(new Color32(30, 30, 30, 255));
    public float             BackgroundBorder    { get; set; } = 5;
    public float             TabButtonSize       { get; set; } = 50;
    public float             TabButtonOffset     { get; set; } = 10;
    public float             TabButtonBorder     { get; set; } = 90;
    public float             TabContainerWidth   { get; set; } = 270;
    public float             TabContainerPadding { get; set; } = 25;
}