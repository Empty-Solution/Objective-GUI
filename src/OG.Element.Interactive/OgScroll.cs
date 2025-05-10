using DK.DataTypes.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgScroll<TElement>(string name, IOgEventHandlerProvider provider)
    : OgInteractableValueElement<TElement, Vector2>(name, provider), IOgVectorValueElement<TElement>, IOgEventCallback<IOgMouseWheelEvent>
    where TElement : IOgElement
{
    public bool Invoke(IOgMouseWheelEvent reason)
    {
        Vector2 value    = Value!.Get();
        Vector2 delta    = reason.Delta;
        Vector2 newValue = new(Mathf.Clamp(value.x + delta.x, Range!.Min.x, Range.Max.x), Mathf.Clamp(value.y + delta.y, Range.Min.y, Range.Max.y));
        if(Equals(value, newValue)) return false;
        Value!.Set(newValue);
        return true;
    }
    public IDkReadOnlyRange<Vector2>? Range { get; set; }
    public override bool Invoke(IOgRenderEvent reason)
    {
        Rect rect = ElementRect;
        rect.position += Value!.Get();
        reason.Enter(rect);
        bool isUsed = ProcessElementsEventForward(reason);
        reason.Exit();
        return isUsed;
    }
}