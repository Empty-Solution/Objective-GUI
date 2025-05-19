using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
using System.Linq;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgTextField<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<string> value,
    IOgTextController textController, Font font)
    : OgFocusableElement<TElement, string>(name, provider, rectGetter, value), IOgTextField<TElement>, IOgEventCallback<IOgKeyBoardCharacterKeyDownEvent>
    where TElement : IOgVisualElement
{
    protected IOgGraphicsContext? Context        => Elements.FirstOrDefault()?.Context;
    public    IOgTextController   TextController => textController;
    public    Font                Font           => font;
    public virtual bool Invoke(IOgKeyBoardCharacterKeyDownEvent reason)
    {
        if(!IsFocusing) return false;
        IOgGraphicsContext? context = Context;
        if(context is null) return false;
        if(UpdateTextIfNeeded(TextController.HandleKeyEvent(Value.Get(), reason, context))) return true;
        char chr = reason.Character;
        return Font.HasCharacter(chr) && UpdateTextIfNeeded(TextController.HandleCharacter(Value.Get(), chr, context));
    }
    protected override bool OnFocus(IOgMouseKeyUpEvent reason)
    {
        IOgGraphicsContext? context = Context;
        if(context is null) return false;
        TextController.TextCursorController.ChangeCursorAndSelectionPositions(Value.Get(), reason.LocalMousePosition, context);
        return true;
    }
    protected override bool OnLostFocus(IOgMouseKeyUpEvent reason)
    {
        IOgGraphicsContext? context = Context;
        if(context is null) return false;
        TextController.TextCursorController.ChangeCursorAndSelectionPositions(Value.Get(), reason.LocalMousePosition, context);
        return true;
    }
    protected override bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        base.BeginControl(reason);
        IOgGraphicsContext? context = Context;
        if(context is null) return false;
        TextController.TextCursorController.ChangeCursorPosition(Value.Get(), reason.LocalMousePosition, context);
        return true;
    }
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        base.EndControl(reason);
        IOgGraphicsContext? context = Context;
        if(context is null) return false;
        TextController.TextCursorController.ChangeSelectionPosition(Value.Get(), reason.LocalMousePosition, context);
        return true;
    }
    private bool UpdateTextIfNeeded(string newValue) => !Equals(Value.Get(), newValue) && Value.Set(newValue);
}