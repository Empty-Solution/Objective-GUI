using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public class EhColorPicker(IOgContainer<IOgElement> sourceContainer, IOgOptionsContainer optionsContainer)
    : EhElement(sourceContainer, optionsContainer), IEhColorPicker
{
}