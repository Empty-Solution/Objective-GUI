using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Element.Interactive;
public class OgClickableElement<TElement>(string name, IOgEventHandlerProvider provider)
    : OgInteractableElement<TElement>(name, provider), IOgClickableElement<TElement> where TElement : IOgElement
{
    public event IOgClickableElement<TElement>.OgClickHandler? OnClicked;
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        base.EndControl(reason);
        OnClicked?.Invoke(this, reason);
        return true;
    }
}