using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhConfigProvider
{
    public DkProperty<float>           AnimationSpeed            { get; set; } = new(0.7f);
    public EhSliderConfig              SliderConfig              { get; set; } = new();
    public EhToggleConfig              ToggleConfig              { get; set; } = new();
    public EhWindowConfig              WindowConfig              { get; set; } = new();
    public EhInteractableElementConfig InteractableElementConfig { get; set; } = new();
    public EhTabButtonConfig           TabButtonConfig           { get; set; } = new();
    public EhTabConfig                 TabConfig                 { get; set; } = new();
    public EhDropdownConfig            DropdownConfig            { get; set; } = new();
    public EhPickerConfig              PickerConfig              { get; set; } = new();
    public float                       SeparatorOffset           { get; set; } = 8;
    public DkProperty<Color>           SeparatorColor            { get; set; } = new(new Color32(100, 100, 100, 150));
    public DkProperty<Color>           SeparatorThumbColor       { get; set; } = new(new Color32(255, 255, 255, 255));
    public float                       SeparatorBorder           { get; set; } = 90;
    public float                       SeparatorSize             { get; set; } = 1;
}