using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public class EhContainer(IOgContainer<IOgElement> source, IOgOptionsContainer optionsContainer) : EhElement(source, optionsContainer), IEhContainer
{
    public virtual bool LinkChild(IOgElement element) => source.Add(element);
    public virtual bool UnlinkChild(IOgElement element) => source.Remove(element);
}