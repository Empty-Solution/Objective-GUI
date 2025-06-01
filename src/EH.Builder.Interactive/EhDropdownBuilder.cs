using DK.Getting.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
namespace EH.Builder.Interactive;
public class EhDropdownBuilder(IEhConfigProvider provider, EhInternalDropdownBuilder dropdownBuilder, EhBaseTextBuilder textBuilder)
{
    public IEhDropdown Build(IDkGetProvider<string> name, IEhProperty<int> selected, IDkGetProvider<string>[] values, float y)
    {
        EhDropdownConfig dropdownConfig = provider.DropdownConfig;
        IEhDropdown dropdown = dropdownBuilder.Build(name.Get(), selected, values, provider.InteractableElementConfig.Width,
            provider.InteractableElementConfig.Height, provider.InteractableElementConfig.Width - dropdownConfig.Width, y);
        dropdown.LinkChild(textBuilder.Build($"{name}NameText", dropdownConfig.NameTextColor, name, dropdownConfig.NameTextFontSize,
            dropdownConfig.NameTextAlignment, provider.InteractableElementConfig.Width - dropdownConfig.Width, provider.InteractableElementConfig.Height));
        return dropdown;
    }
}