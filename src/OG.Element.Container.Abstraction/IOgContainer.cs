using OG.Element.Abstraction;
using System.Collections.Generic;
namespace OG.Element.Container.Abstraction;
public interface IOgContainer<TElement> : IOgElement where TElement : IOgElement
{
    IEnumerable<TElement> Elements { get; }
    bool                  Contains(TElement element);
    bool                  Add(TElement      element);
    bool                  Remove(TElement   element);
}