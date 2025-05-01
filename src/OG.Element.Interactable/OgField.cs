
using OG.Element.Abstraction;
using OG.Element.Control.Focusable;
using OG.Element.Interactable.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.TextController.Abstraction;

namespace OG.Element.Interactable;

public abstract class OgField<TElement>(IOgEventProvider eventProvider, IOgTextController controller) : OgFocusableControl<TElement, string>(eventProvider), IOgField<TElement> where TElement : IOgElement
{
    protected override bool OnFocus(IOgMouseKeyUpEvent reason)
    {
        controller.TextCursorController.ChangeCursorAndSelectionPositions(Value!.Get(), Rectangle!.Get(), reason);
        reason.Consume();
        return true;
    }

    protected override bool OnLostFocus(IOgMouseKeyUpEvent reason)
    {
        controller.TextCursorController.ChangeCursorAndSelectionPositions(Value!.Get(), Rectangle!.Get(), reason);
        reason.Consume();
        return true;
    }

    protected override bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        if(!base.BeginControl(reason)) return false;
        controller.TextCursorController.ChangeCursorPosition(Value!.Get(), Rectangle!.Get(), reason);
        reason.Consume();
        return true;
    }

    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        if(!base.EndControl(reason)) return false;
        controller.TextCursorController.ChangeSelectionPosition(Value!.Get(), Rectangle!.Get(), reason);
        reason.Consume();
        return true;
    }

    protected virtual bool HandleKeyDown(IOgKeyDownEvent reason)
    {
        if(!IsFocused) return true;
        if(UpdateTextIfNeeded(controller.HandleKeyEvent(Value!.Get(), reason), reason)) return true;
        char chr = reason.Character;
        return HasCharacter(chr) && UpdateTextIfNeeded(controller.HandleCharacter(Value!.Get(), chr), reason);
    }

    private bool UpdateTextIfNeeded(string newValue, IOgEvent reason)
    {
        if(Equals(Value!.Get(), newValue)) return false;
        if(!ChangeValue(newValue)) return false;
        reason.Consume();
        return true;
    }

    protected abstract bool HasCharacter(char chr);

    private class OgKeyDownEventHandler(OgField<TElement> owner) : OgEventHandlerBase<IOgKeyDownEvent>
    {
        public override bool Handle(IOgKeyDownEvent reason) => owner.HandleKeyDown(reason);
    }
}