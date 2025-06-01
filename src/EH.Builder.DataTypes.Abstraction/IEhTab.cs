using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.DataTypes;
public interface IEhTab : IEhContainer
{
    IOgToggle<IOgVisualElement> Button           { get; }
    IOgContainer<IOgElement>    TabContainer     { get; }
    IOgContainer<IOgElement>    ToolbarContainer { get; }
    IEnumerable<IEhSubTab>      SubTabs          { get; }
    void AddSubTab(IEhSubTab subtab);
}