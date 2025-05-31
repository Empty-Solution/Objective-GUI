using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Wrapping.DataTypes;
public class EhSourceTab(IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> toolbar, IOgToggle<IOgVisualElement> button)
{
    private readonly List<EhSubTab>              m_SubTabs = [];
    public           IEnumerable<EhSubTab>       SubTabs         => m_SubTabs;
    public           IOgContainer<IOgElement>    Toolbar         { get; } = toolbar;
    public           IOgContainer<IOgElement>    SourceContainer { get; } = sourceContainer;
    public           IOgToggle<IOgVisualElement> Button          { get; } = button;
    public void AddSubTab(EhSubTab subTab) => m_SubTabs.Add(subTab);
    public void RemoveSubTab(EhSubTab subTab) => m_SubTabs.Add(subTab);
}