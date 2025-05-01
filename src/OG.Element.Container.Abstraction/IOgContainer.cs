
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using System.Collections.Generic;

namespace OG.Element.Container.Abstraction;

public interface IOgContainer<TElement> : IOgElement where TElement : IOgElement
{
    IEnumerable<TElement> Elements { get; }

    bool Contains(TElement element);

    void Add(TElement element);

    void Remove(TElement element);
    bool ProcElementsBackward(IOgInputEvent reason);
}