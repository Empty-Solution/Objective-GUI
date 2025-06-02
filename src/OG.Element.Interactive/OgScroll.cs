using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Setting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgScroll<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkSetProvider<Rect> elementRectSetter,
    IDkFieldProvider<Vector2> value)
    : OgInteractableValueElement<TElement, Vector2>(name, provider, rectGetter, value), IOgVectorValueElement<TElement>,
      IOgEventCallback<IOgMouseWheelEvent> where TElement : IOgElement
{
    public bool Invoke(IOgMouseWheelEvent reason)
    {
        Vector2 value = Value.Get();
        Vector2 newValue = new(Mathf.Clamp(value.x + reason.Delta.x, Range!.Min.x, Range.Max.x),
            Mathf.Clamp(value.y + reason.Delta.y, Range.Min.y, Range.Max.y));
        if(Equals(value, newValue)) return false;
        Rect rect = ElementRect.Get();
        rect.position += newValue;
        Value.Set(Vector2.zero);
        return elementRectSetter.Set(rect) || base.Invoke(reason);
    }
    public IDkReadOnlyRange<Vector2>? Range { get; set; }
    public override bool Invoke(IOgRenderEvent reason)
    {
        Rect rect = ElementRect.Get();
        reason.Enter(rect);
        reason.Global += rect.position + Value.Get();
        ProcessElementsEventForwardWithDelta(reason);
        reason.Exit();
        reason.Global -= rect.position + Value.Get();
        return false;
    }
    public override bool Invoke(IOgInputEvent reason)
    {
        Rect rect = ElementRect.Get();
        reason.LocalMousePosition -= rect.position + Value.Get();
        bool isUsed = ProcessElementsEventBackwardWithDelta(reason);
        reason.LocalMousePosition += rect.position + Value.Get();
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
        if(elementRect.yMax > rect.yMax || elementRect.xMax > rect.xMax) return false;
        return element.ProcessEvent(reason);
    }
}