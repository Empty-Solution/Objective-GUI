using OG.Element.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.Container.Abstraction;

public interface IOgContainer<TElement> : IOgElement where TElement : IOgElement
{
    IEnumerable<TElement> Elements { get; }

    bool Contains(TElement element);

    void Add(TElement element);

    void Remove(TElement element);
    bool ProcElementsBackward(IOgInputEvent reason);
}