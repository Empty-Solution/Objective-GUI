using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public class EhElement(IOgElement source, IOgOptionsContainer optionsContainer) : IEhElement
{
    public IOgOptionsContainer OptionsContainer { get; } = optionsContainer;
    public virtual bool LinkSelf(IOgContainer<IOgElement> container) => container.Add(source);
    public virtual bool UnlinkSelf(IOgContainer<IOgElement> container) => container.Remove(source);
}