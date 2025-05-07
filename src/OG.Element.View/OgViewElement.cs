using DK.Getting.Abstraction.Generic;
using OG.Element.View.Abstraction;
using OG.Element.Visual;
using OG.Event.Abstraction;
namespace OG.Element.View;
public abstract class OgViewElement<TViewValue>(string name, IOgEventHandlerProvider provider, IDkGetProvider<TViewValue> view)
    : OgVisualElement(name, provider), IOgViewElement<TViewValue>
{
    public IDkGetProvider<TViewValue> View => view;
}