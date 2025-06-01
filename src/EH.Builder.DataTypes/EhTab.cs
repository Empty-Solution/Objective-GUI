using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.DataTypes;
public class EhTab(IOgToggle<IOgVisualElement> button, IOgContainer<IOgElement> tabContainer, IOgContainer<IOgElement> toolbarContainer)
    : EhContainer(tabContainer), IEhTab
{
    private readonly List<IEhSubTab>             m_SubTabs = [];
    public           IEnumerable<IEhSubTab>      SubTabs          => m_SubTabs;
    public           IOgToggle<IOgVisualElement> Button           { get; } = button;
    public           IOgContainer<IOgElement>    TabContainer     { get; } = tabContainer;
    public           IOgContainer<IOgElement>    ToolbarContainer { get; } = toolbarContainer;
    public void AddSubTab(IEhSubTab subtab)
    {
        m_SubTabs.Add(subtab);
        subtab.LinkSelf(TabContainer);
    }
}