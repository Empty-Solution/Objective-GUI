using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
using System;
namespace EH.Builder.DataTypes;
public class EhDropdown(IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> dropdownContainer, IOgOptionsContainer optionsContainer,
    Func<IDkGetProvider<string>, IOgInteractableElement<IOgVisualElement>> buildAction) : EhContainer(sourceContainer), IEhDropdown
{
    public IOgOptionsContainer OptionsContainer { get; } = optionsContainer;
    public void AddItem(IDkGetProvider<string> name) => dropdownContainer.Add(buildAction(name));
}