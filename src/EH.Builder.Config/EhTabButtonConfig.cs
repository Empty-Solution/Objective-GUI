using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Config;
public class EhTabButtonConfig
{
    public DkProperty<Color> ButtonColor     { get; }      = new(new Color32(150, 150, 150, 150));
    public DkProperty<Color> InteractColor   { get; }      = new(Color.white);
    public float             TabButtonSize   { get; set; } = 50;
    public float             TabButtonOffset { get; set; } = 10;
    public float             TabButtonBorder { get; set; } = 90;
}