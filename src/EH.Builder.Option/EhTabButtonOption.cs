using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhTabButtonOption
{
    public DkProperty<Color> BackgroundColorProperty         { get; }      = new(new Color32(150, 150, 150, 150));
    public DkProperty<Color> BackgroundInteractColorProperty { get; }      = new(Color.white);
    public float             TabButtonSize                   { get; set; } = 50;
    public float             TabButtonBorder                 { get; set; } = 90;
    public float             TabContainerWidth               { get; set; } = 560;
}