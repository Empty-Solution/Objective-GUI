using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Interactive;
public class EhDropdownBuilder(IEhConfigProvider provider, EhInternalDropdownBuilder dropdownBuilder, EhBaseTextBuilder textBuilder)
{
    public IOgContainer<IOgElement> Build(IDkGetProvider<string> name, IDkProperty<int> selected, IEnumerable<IDkGetProvider<string>> values, float y)
    {
        EhDropdownConfig dropdownConfig = provider.DropdownConfig;
        IOgContainer<IOgElement> container = dropdownBuilder.Build(name.Get(), selected, values, provider.InteractableElementConfig.Width,
            provider.InteractableElementConfig.Height, provider.InteractableElementConfig.Width - dropdownConfig.Width, y, out _);
        container.Add(textBuilder.Build($"{name}NameText", dropdownConfig.NameTextColor, name, dropdownConfig.NameTextFontSize,
            dropdownConfig.NameTextAlignment, provider.InteractableElementConfig.Width - dropdownConfig.Width, provider.InteractableElementConfig.Height));
        return container;
    }
}