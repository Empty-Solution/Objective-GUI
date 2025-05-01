using OG.Element.Abstraction;
using OG.Element.Control;
using OG.Element.Interactable.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.Interactable;

public class OgClickable<TElement>(IOgEventProvider eventProvider) : OgControl<TElement>(eventProvider), IOgClickable<TElement> where TElement : IOgElement
{
    public event IOgClickable<TElement>.OgClickHandler? OnClicked;

    protected override bool EndControl(IOgMouseKeyUpEvent reason) => base.EndControl(reason) && Click(reason);

    protected virtual bool Click(IOgMouseKeyUpEvent reason)
    {
        if(OnClicked is null) return false;
        OnClicked?.Invoke(this, reason);
        return true;
    }
}