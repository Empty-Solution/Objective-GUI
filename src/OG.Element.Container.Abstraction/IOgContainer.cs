using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
using System.Collections.Generic;
namespace OG.Element.Container.Abstraction;
public interface IOgContainer<TElement> : IOgElement, IOgMouseEventHandler where TElement : IOgElement
{
    IEnumerable<TElement> Elements { get; }
    bool Contains(TElement element);
    void Add(TElement element);
    void Remove(TElement element);
    bool ProcElementsBackward(IOgInputEvent reason);
    bool ProcElementsForward(IOgEvent reason);
}