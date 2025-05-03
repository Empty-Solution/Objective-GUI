using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace OG.Element.Hoverable.Abstraction;
public interface IOgHoverable<TElement> : IOgContainer<TElement> where TElement : IOgElement
{
    bool IsHovered { get; }
}