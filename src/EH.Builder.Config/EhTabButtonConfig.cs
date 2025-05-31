using DK.Property.Generic;
using EH.Builder.Config.Abstraction;
using UnityEngine;
namespace EH.Builder.Config;
public class EhTabButtonConfig : IEhElementConfig
{
    public DkProperty<Color> ButtonColor     { get; }      = new(new Color32(150, 150, 150, 150));
    public DkProperty<Color> InteractColor   { get; }      = new(Color.white);
    public float             TabButtonOffset { get; set; } = 10;
    public float             TabButtonBorder { get; set; } = 90;
    public float             Width           { get; set; } = 50;
    public float             Height          { get; set; } = 50;
}