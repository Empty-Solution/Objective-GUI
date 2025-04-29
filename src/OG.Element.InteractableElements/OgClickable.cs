using OG.Element.Abstraction;
using OG.Element.Control;
using OG.Element.Control.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.InteractableElements;

public class OgClickable<TElement>(IOgEventProvider eventProvider) : OgControl<TElement>(eventProvider), IOgClickable<TElement>
    where TElement : IOgElement
{
    public event IOgClickable<TElement>.OgClickHandler? OnClicked;
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        base.EndControl(reason);
        Click(reason);
        return true;
    }
    
    protected virtual void Click(IOgMouseKeyUpEvent reason) => OnClicked?.Invoke(this, reason);
}