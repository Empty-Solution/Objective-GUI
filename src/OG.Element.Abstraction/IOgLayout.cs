using DK.Scoping.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgLayout<TElement> : IDkScope where TElement : IOgElement
{
    void ProcessItem(TElement element);
}