using DK.Property.Generic;
using EH.Builder.Providing.Abstraction;
using UnityEngine;
namespace EH.Builder.Config;
public class EhConfigProvider : IEhConfigProvider
{
    public DkProperty<float>           AnimationSpeed            { get; }      = new(0.7f);
    public EhSliderConfig              SliderConfig              { get; set; } = new();
    public EhToggleConfig              ToggleConfig              { get; set; } = new();
    public EhMainWindowConfig          MainWindowConfig          { get; set; } = new();
    public EhInteractableElementConfig InteractableElementConfig { get; set; } = new();
    public EhTabButtonConfig           TabButtonConfig           { get; set; } = new();
    public EhTabConfig                 TabConfig                 { get; set; } = new();
    public EhDropdownConfig            DropdownConfig            { get; set; } = new();
    public EhPickerConfig              PickerConfig              { get; set; } = new();
    public float                       SeparatorOffset           { get; set; } = 8;
    public DkProperty<Color>           SeparatorColor            { get; }      = new(new Color32(100, 100, 100, 150));
    public DkProperty<Color>           SeparatorThumbColor       { get; }      = new(new Color32(255, 255, 255, 255));
    public float                       SeparatorBorder           { get; set; } = 90;
    public float                       SeparatorSize             { get; set; } = 1;
}