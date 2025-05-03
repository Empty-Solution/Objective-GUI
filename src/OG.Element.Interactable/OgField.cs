using DK.Property.Abstraction.Generic;
using OG.DataTypes.Font.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Control.Focusable;
using OG.Element.Interactable.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction.Contexts;
using OG.TextController.Abstraction;
// TODO: Нужно избегать подобной зависимости!
// using OG.Element.Visual.Abstraction;
namespace OG.Element.Interactable;
public class OgField<TElement> : OgFocusableControl<TElement, string>, IOgField<TElement>, IOgElementEventHandler<IOgKeyDownEvent>,
                                 IOgElementEventHandler<IOgRepaintEvent> where TElement : IOgElement
{
    // TODO: Функция может работь не корректно!
    private readonly OgTextRepaintContext m_Context = new();
    private readonly IOgTextController    m_Controller;
    protected OgField(IOgEventProvider eventProvider, IOgTextController controller) : base(eventProvider)
    {
        m_Controller = controller;
        eventProvider.RegisterHandler(new OgEventHandler<IOgKeyDownEvent>(this));
        eventProvider.RegisterHandler(new OgEventHandler<IOgRepaintEvent>(this));
    }
    public IDkProperty<IOgFont>? Font { get; set; }
    // TODO: Тут был абсолютный бред. Пришлось закоментировать. ИСПОЛЬЗОВАТЬ: IOgElementEventHandler<IOgRepaintEvent>.HandleEvent
    /*
    public bool HandleRepaint(IOgTextRepaintEvent reason)
    {

        foreach(TElement? element in Elements)
            if(ShouldProcElement(element))
                m_Context = element.HandleRepaint(reason);
        return true;


        return true;
    }
    */
    bool IOgElementEventHandler<IOgKeyDownEvent>.HandleEvent(IOgKeyDownEvent reason)
    {
        if(!IsFocused) return false;
        if(UpdateTextIfNeeded(m_Controller.HandleKeyEvent(Value!.Get(), reason), reason)) return false;
        char chr = reason.Character;
        return Font.Get().HasCharacter(chr) && UpdateTextIfNeeded(m_Controller.HandleCharacter(Value!.Get(), chr), reason);
    }
    // TODO: Реализовать устаревший метод HandleRepaint.
    bool IOgElementEventHandler<IOgRepaintEvent>.HandleEvent(IOgRepaintEvent reason) => true;
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
}