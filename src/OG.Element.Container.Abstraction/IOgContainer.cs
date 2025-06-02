using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using System.Collections.Generic;
namespace OG.Element.Container.Abstraction;
public interface IOgContainer<TElement> : IOgElement, IOgEventCallback<IOgRenderEvent>, IOgEventCallback<IOgLayoutEvent> where TElement : IOgElement
{
    IEnumerable<TElement> Elements { get; }
    bool                  Sort     { get; set; }
    void Clear();
    bool Add(TElement element);
    bool Remove(TElement element);
    int IndexOf(TElement element);
}