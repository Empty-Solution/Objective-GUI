using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhToggle : IEhElement
{
    IOgContainer<IOgElement> SourceContainer { get; }
}