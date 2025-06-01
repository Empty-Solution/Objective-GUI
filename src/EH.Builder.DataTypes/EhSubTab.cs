using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.DataTypes;
public class EhSubTab(IOgContainer<IOgElement> sourceContainer) : EhElement(sourceContainer), IEhSubTab
{
    private readonly List<IEhTabGroup>        m_Groups = [];
    public           IEnumerable<IEhTabGroup> Groups => m_Groups;
    public void AddGroup(IEhTabGroup group)
    {
        m_Groups.Add(group);
        group.LinkSelf(sourceContainer);
    }
    public void RemoveGroup(IEhTabGroup group)
    {
        m_Groups.Remove(group);
        group.UnlinkSelf(sourceContainer);
    }
}