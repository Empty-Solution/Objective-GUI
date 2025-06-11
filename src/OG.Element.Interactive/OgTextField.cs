using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
using System.Linq;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgTextField : OgFocusableElement<IOgTextElement, string>, IOgTextField<IOgTextElement>, IOgEventCallback<IOgKeyBoardCharacterKeyDownEvent>,
                           IOgEventCallback<IOgKeyBoardKeyDownEvent>
{
    public OgTextField(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<string> value,
        IOgTextController textController) : base(name, provider, rectGetter, value)
    {
        TextController = textController;
        provider.Register<IOgKeyBoardKeyDownEvent>(this);
        provider.Register<IOgKeyBoardCharacterKeyDownEvent>(this);
    }
    public IOgTextController TextController { get; }
    public virtual bool Invoke(IOgKeyBoardCharacterKeyDownEvent reason)
    {
        if(!IsFocusing) return false;
        IOgTextGraphicsContext? context = Context;
        if(context is null) return false;
        char chr = reason.Character;
        return (context.Font?.HasCharacter(chr) ?? false) && UpdateTextIfNeeded(TextController.HandleCharacter(Value.Get(), chr, context));
    }
    public bool Invoke(IOgKeyBoardKeyDownEvent reason)
    {
        if(!IsFocusing) return false;
        IOgTextGraphicsContext? context = Context;
        if(context is null) return false;
        string got = Value.Get();
        if(!TextController.HandleKeyEvent(got, reason, context, out string text)) return false;
        UpdateTextIfNeeded(text);
        return true;
    }
    public IOgTextGraphicsContext? Context => Elements.FirstOrDefault()?.Context;
    protected override bool OnFocus(IOgMouseKeyUpEvent reason)
    {
        IOgTextGraphicsContext? context = Context;
        if(context is null) return false;
        TextController.ChangeCursorAndSelectionPositions(Value.Get(), reason.GlobalMousePosition, context);
        return true;
    }
    protected override bool OnLostFocus(IOgMouseKeyUpEvent reason)
    {
        IOgTextGraphicsContext? context = Context;
        if(context is null) return false;
        TextController.ChangeCursorAndSelectionPositions(Value.Get(), reason.GlobalMousePosition, context);
        return true;
    }
    protected override bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        IOgTextGraphicsContext? context = Context;
        if(context is null) return IsFocusing || base.BeginControl(reason);
        base.BeginControl(reason);
        TextController.ChangeCursorPosition(Value.Get(), reason.GlobalMousePosition, context);
        return true;
    }
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IOgTextGraphicsContext? context = Context;
        if(context is null) return base.EndControl(reason);
        base.EndControl(reason);
        TextController.ChangeSelectionPosition(Value.Get(), reason.GlobalMousePosition, context);
        return true;
    }
    private bool UpdateTextIfNeeded(string newValue) => !Equals(Value.Get(), newValue) && Value.Set(newValue);
}