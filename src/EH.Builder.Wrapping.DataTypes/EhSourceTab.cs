using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Wrapping.DataTypes;
public class EhSourceTab(IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> toolbar,
    IOgToggle<IOgVisualElement> button)
{
    private readonly List<EhTab>                 m_Tabs = [];
    public           IEnumerable<EhTab>          Tabs            => m_Tabs;
    public           IOgContainer<IOgElement>    Toolbar         { get; } = toolbar;
    public           IOgContainer<IOgElement>    SourceContainer { get; } = sourceContainer;
    public           IOgToggle<IOgVisualElement> Button          { get; } = button;
    public void AddTab(EhTab tab) => m_Tabs.Add(tab);
    public void RemoveTab(EhTab tab) => m_Tabs.Add(tab);
}