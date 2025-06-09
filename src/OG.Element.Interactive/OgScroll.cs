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
        Vector2 value    = Value.Get();
        Vector2 newValue = new(0, Mathf.Clamp(value.y + (reason.Delta.y * ScrollMultiplier), Range!.Get().Min.y, Range.Get().Max.y));
        if(Equals(value, newValue)) return false;
        Value.Set(newValue);
        return base.Invoke(reason);
    }
    public override bool Invoke(IOgRenderEvent reason)
    {
        Rect rect = ElementRect.Get();
        reason.Global += rect.position - Value.Get();
        ProcessElementsEventForwardWithDelta(reason);
        reason.Global -= rect.position - Value.Get();
        return false;
    }
    public IDkGetProvider<IDkReadOnlyRange<Vector2>>? Range            { get; set; }
    public float                                      ScrollMultiplier { get; set; } = 2.5f;
    public override bool Invoke(IOgInputEvent reason)
    {
        if(reason is IOgKeyBoardEvent) return base.Invoke(reason);
        if(!IsHovering) return false;
        Rect rect = ElementRect.Get();
        reason.LocalMousePosition -= rect.position - Value.Get();
        bool isUsed = ProcessElementsEventBackwardWithDelta(reason);
        reason.LocalMousePosition += rect.position - Value.Get();
        return isUsed;
    }
    protected bool ProcessElementsEventForwardWithDelta(IOgEvent reason)
    {
        Rect rect = ElementRect.Get();
        rect.position += Value.Get();
        for(int i = 0; i < m_Elements.Count; i++)
            if(ProcessElement(reason, rect, m_Elements[i]))
                return true;
        return false;
    }
    private bool ProcessElementsEventBackwardWithDelta(IOgInputEvent reason)
    {
        Rect rect = ElementRect.Get();
        rect.position += Value.Get();
        for(int i = m_Elements.Count - 1; i >= 0; i--)
            if(ProcessElement(reason, rect, m_Elements[i]))
                return true;
        return false;
    }
    private bool ProcessElement(IOgEvent reason, Rect rect, TElement element)
    {
        Rect elementRect = element.ElementRect.Get();
        if(elementRect.yMin + 10 < rect.yMin || elementRect.yMax - 10 > rect.yMax) return false;
        return element.ProcessEvent(reason);
    }
}