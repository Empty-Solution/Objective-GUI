using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Wrapping.DataTypes;
public class EhSubTab(IOgContainer<IOgElement> sourceContainer)
{
    private readonly List<IOgContainer<IOgElement>>        m_Groups = [];
    public           IEnumerable<IOgContainer<IOgElement>> Groups          => m_Groups;
    public           IOgContainer<IOgElement>              SourceContainer { get; } = sourceContainer;
    public void AddGroup(IOgContainer<IOgElement> group) => m_Groups.Add(group);
    public void RemoveGroup(IOgContainer<IOgElement> group) => m_Groups.Add(group);
}