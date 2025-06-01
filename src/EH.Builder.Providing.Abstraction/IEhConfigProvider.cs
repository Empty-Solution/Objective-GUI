using DK.Property.Generic;
using EH.Builder.Config;
using UnityEngine;
namespace EH.Builder.Providing.Abstraction;
public interface IEhConfigProvider
{
    DkProperty<float>           AnimationSpeed            { get; }
    EhSliderConfig              SliderConfig              { get; set; }
    EhToggleConfig              ToggleConfig              { get; set; }
    EhMainWindowConfig          MainWindowConfig          { get; set; }
    EhInteractableElementConfig InteractableElementConfig { get; set; }
    EhTabButtonConfig           TabButtonConfig           { get; set; }
    EhButtonConfig              ButtonConfig              { get; set; }
    EhTabGroupConfig            TabGroupConfig            { get; set; }
    EhDropdownConfig            DropdownConfig            { get; set; }
    EhPickerConfig              PickerConfig              { get; set; }
    float                       SeparatorOffset           { get; set; }
    DkProperty<Color>           SeparatorColor            { get; }
    DkProperty<Color>           SeparatorThumbColor       { get; }
    float                       SeparatorBorder           { get; set; }
    float                       SeparatorSize             { get; set; }
}