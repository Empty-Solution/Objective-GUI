using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhElement
{
    IOgOptionsContainer OptionsContainer { get; }
    bool LinkSelf(IOgContainer<IOgElement> container);
    bool UnlinkSelf(IOgContainer<IOgElement> container);
}