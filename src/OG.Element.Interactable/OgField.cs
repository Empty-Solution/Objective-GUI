using OG.Element.Control.Focusable;
using OG.Element.Interactable.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
namespace OG.Element.Interactable;
public abstract class OgField<TElement> : OgFocusableControl<TElement, string>, IOgField<TElement> where TElement : IOgText
{
    private readonly IOgTextController    m_Controller;
    private          OgTextRepaintContext m_Context;
    protected OgField(IOgEventProvider eventProvider, IOgTextController controller) : base(eventProvider)
    {
        m_Controller = controller;
        eventProvider.RegisterHandler(new OgKeyDownEventHandler(this));
        eventProvider.RegisterHandler(new OgTextRepaintEventHandler(this));
    }
    public virtual bool HandleKeyDown(IOgKeyDownEvent reason)
    {
        if(!IsFocused) return true;
        if(UpdateTextIfNeeded(m_Controller.HandleKeyEvent(Value!.Get(), reason), reason)) return true;
        char chr = reason.Character;
        return HasCharacter(chr) && UpdateTextIfNeeded(m_Controller.HandleCharacter(Value!.Get(), chr), reason);
    }
    protected override bool OnFocus(IOgMouseKeyUpEvent reason)
    {
        m_Controller.TextCursorController.ChangeCursorAndSelectionPositions(Value!.Get(), reason, m_Context);
        reason.Consume();
        return true;
    }
    protected override bool OnLostFocus(IOgMouseKeyUpEvent reason)
    {
        m_Controller.TextCursorController.ChangeCursorAndSelectionPositions(Value!.Get(), reason, m_Context);
        reason.Consume();
        return true;
    }
    protected override bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        if(!base.BeginControl(reason)) return false;
        m_Controller.TextCursorController.ChangeCursorPosition(Value!.Get(), reason, m_Context);
        reason.Consume();
        return true;
    }
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        if(!base.EndControl(reason)) return false;
        m_Controller.TextCursorController.ChangeSelectionPosition(Value!.Get(), reason, m_Context);
        reason.Consume();
        return true;
    }
    private bool UpdateTextIfNeeded(string newValue, IOgEvent reason)
    {
        if(Equals(Value!.Get(), newValue)) return false;
        if(!ChangeValue(newValue)) return false;
        reason.Consume();
        return true;
    }
    protected abstract bool HasCharacter(char chr);
    public bool HandleRepaint(IOgTextRepaintEvent reason)
    {
        foreach(TElement? element in Elements)
            if(ShouldProcElement(reason, element))
                m_Context = element.HandleRepaint(reason);
        return true;
    }
    public class OgKeyDownEventHandler(IOgField<TElement> owner) : OgRecallInputEventHandler<IOgKeyDownEvent>(owner)
    {
        public override bool Handle(IOgKeyDownEvent reason) => owner.HandleKeyDown(reason);
    }
    public class OgTextRepaintEventHandler(OgField<TElement> owner) : OgEventHandlerBase<IOgTextRepaintEvent>
    {
        public override bool Handle(IOgTextRepaintEvent reason) => owner.HandleRepaint(reason);
    }
}