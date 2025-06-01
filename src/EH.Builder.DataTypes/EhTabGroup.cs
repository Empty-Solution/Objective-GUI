using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public class EhTabGroup(IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> groupContainer, IOgOptionsContainer optionsContainer)
    : EhContainer(sourceContainer, optionsContainer), IEhTabGroup
{
    public IOgContainer<IOgElement> GroupContainer { get; } = groupContainer;
    public override bool LinkChild(IOgElement element) => GroupContainer.Add(element);
    public override bool UnlinkChild(IOgElement element) => GroupContainer.Remove(element);
}