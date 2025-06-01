using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
using System;
using System.Collections.Generic;
namespace EH.Builder.DataTypes;
public class EhDropdown(IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> dropdownContainer, IList<IDkGetProvider<string>> values,
    IOgOptionsContainer optionsContainer, Func<IDkGetProvider<string>, IOgInteractableElement<IOgVisualElement>> buildAction)
    : EhContainer(sourceContainer, optionsContainer), IEhDropdown
{
    public void AddItem(IDkGetProvider<string> name)
    {
        dropdownContainer.Add(buildAction(name));
        values.Add(name);
    }
}