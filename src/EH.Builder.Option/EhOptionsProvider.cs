using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Option;
public class EhOptionsProvider
{
    public DkProperty<float>           AnimationSpeed            { get; set; } = new(0.7f);
    public EhSliderOption              SliderOption              { get; }      = new();
    public EhToggleOption              ToggleOption              { get; }      = new();
    public EhWindowOption              WindowOption              { get; }      = new();
    public EhInteractableElementOption InteractableElementOption { get; }      = new();
    public EhTabButtonOption           TabButtonOption           { get; }      = new();
    public EhDropdownOption            DropdownOption            { get; }      = new();
    public float                       SeparatorOffset           { get; set; } = 8;
    public DkProperty<Color>           SeparatorColor            { get; }      = new(new Color32(100, 100, 100, 150));
    public DkProperty<Color>           SeparatorThumbColor       { get; }      = new(new Color32(255, 255, 255, 255));
    public float                       SeparatorBorder           { get; }      = 90;
    public float                       SeparatorWidth            { get; }      = 1;
}