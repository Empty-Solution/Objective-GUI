using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public class EhTabGroup(IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> groupContainer) : EhContainer(sourceContainer), IEhTabGroup
{
    public IOgContainer<IOgElement> GroupContainer { get; } = groupContainer;
}