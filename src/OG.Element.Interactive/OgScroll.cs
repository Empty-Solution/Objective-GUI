using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgScroll<TElement> : OgInteractableValueElement<TElement, Vector2>, IOgScroll<TElement>, IOgEventCallback<IOgMouseWheelEvent>
    where TElement : IOgElement
{
    public OgScroll(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<Vector2> value) : base(name, provider,
        rectGetter, value) =>
        provider.Register<IOgMouseWheelEvent>(this);
    public bool Invoke(IOgMouseWheelEvent reason)
    {
        if(!IsHovering) return false;
        var value = Value.Get();
        Vector2 newValue = new(0, Mathf.Clamp(value.y + (reason.Delta.y * ScrollMultiplier), Range!.Get().Min.y, Range.Get().Max.y));
        if(Equals(value, newValue)) return false;
        _ = Value.Set(newValue);
        return base.Invoke(reason);
    }
    public override bool Invoke(IOgRenderEvent reason)
    {
        var rect = ElementRect.Get();
        reason.Enter(rect, Value.Get());
        _ = ProcessElementsEventForwardWithDelta(reason);
        reason.Exit();
        return false;
    }
    public IDkGetProvider<IDkReadOnlyRange<Vector2>>? Range { get; set; }
    public float ScrollMultiplier { get; set; } = 3f;
    protected override bool PreBeginControl(IOgMouseKeyDownEvent reason) =>
        HandleInputWidthDelta(reason) || (IsHovering && !IsInteracting && BeginControl(reason));
    protected override bool PreEndControl(IOgMouseKeyUpEvent reason) => HandleInputWidthDelta(reason) || (IsInteracting && EndControl(reason));
    protected override bool HandleMouseMove(IOgMouseMoveEvent reason) => HandleInputWidthDelta(reason) || HandleHover(reason);
    public override bool Invoke(IOgInputEvent reason) => HandleInputWidthDelta(reason);
    private bool HandleInputWidthDelta(IOgInputEvent reason)
    {
        if(reason is IOgKeyBoardEvent) return base.Invoke(reason);
        if(!IsHovering) return false;
        var rect = ElementRect.Get();
        var value = Value.Get();
        reason.LocalMousePosition -= rect.position - value;
        bool isUsed = ProcessElementsEventBackwardWithDelta(reason);
        reason.LocalMousePosition += rect.position - value;
        return isUsed;
    }
    protected bool ProcessElementsEventForwardWithDelta(IOgEvent reason)
    {
        var rect = ElementRect.Get();
        rect.position += Value.Get();
        for(int i = 0; i < m_Elements.Count; i++)
            if(ProcessElement(reason, rect, m_Elements[i]))
                return true;
        return false;
    }
    private bool ProcessElementsEventBackwardWithDelta(IOgInputEvent reason)
    {
        var rect = ElementRect.Get();
        rect.position += Value.Get();
        for(int i = m_Elements.Count - 1; i >= 0; i--)
            if(ProcessElement(reason, rect, m_Elements[i]))
                return true;
        return false;
    }
    private bool ProcessElement(IOgEvent reason, Rect rect, TElement element)
    {
        var elementRect = element.ElementRect.Get();
        return elementRect.yMin + (elementRect.height / 2) >= rect.yMin && elementRect.yMax - (elementRect.height / 2) <= rect.yMax && element.ProcessEvent(reason);
    }
}