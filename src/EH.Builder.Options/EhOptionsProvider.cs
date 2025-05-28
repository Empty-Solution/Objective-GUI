using DK.Property.Generic;
using UnityEngine;
namespace EH.Builder.Options;
public class EhOptionsProvider
{
    public DkProperty<float>           AnimationSpeed            { get; set; } = new(0.7f);
    public EhSliderOption              SliderOption              { get; set; }      = new();
    public EhToggleOption              ToggleOption              { get; set; }      = new();
    public EhWindowOption              WindowOption              { get; set; }      = new();
    public EhInteractableElementOption InteractableElementOption { get; set; }      = new();
    public EhTabButtonOption           TabButtonOption           { get; set; }      = new();
    public EhTabOption                 TabOption                 { get; set; }      = new();
    public EhDropdownOption            DropdownOption            { get; set; }      = new();
    public EhPickerOption              PickerOption              { get; set; }      = new();
    public float                       SeparatorOffset           { get; set; } = 8;
    public DkProperty<Color>           SeparatorColor            { get; set; }      = new(new Color32(100, 100, 100, 150));
    public DkProperty<Color>           SeparatorThumbColor       { get; set; }      = new(new Color32(255, 255, 255, 255));
    public float                       SeparatorBorder           { get; set; }      = 90;
    public float                       SeparatorWidth            { get; set; }      = 1;
}