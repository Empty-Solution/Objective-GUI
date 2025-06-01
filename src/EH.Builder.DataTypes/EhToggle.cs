using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public class EhToggle(IOgContainer<IOgElement> sourceContainer) : EhElement(sourceContainer), IEhToggle
{
    public IOgContainer<IOgElement> SourceContainer { get; } = sourceContainer;
}