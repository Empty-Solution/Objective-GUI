using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgClickable<TElement, TScope> : IOgContainer<TElement> where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgClickHandler(IOgClickable<TElement, TScope> instance, OgEvent reason);

    public event OgClickHandler? OnClicked;
}