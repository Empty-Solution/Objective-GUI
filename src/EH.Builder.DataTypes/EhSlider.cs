using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public class EhSlider(IOgContainer<IOgElement> sourceContainer) : EhElement(sourceContainer), IEhSlider
{
    public IOgContainer<IOgElement> SourceContainer { get; } = sourceContainer;
}