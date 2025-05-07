using OG.Element.Abstraction;
namespace OG.Element.Container.Abstraction;
public interface IOgContainer<in TElement> : IOgElement where TElement : IOgElement
{
    bool Contains(TElement element);
    bool Add(TElement      element);
    bool Remove(TElement   element);
}