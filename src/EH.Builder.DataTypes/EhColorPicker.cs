using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public class EhColorPicker(IOgContainer<IOgElement> sourceContainer) : EhElement(sourceContainer), IEhColorPicker
{
}