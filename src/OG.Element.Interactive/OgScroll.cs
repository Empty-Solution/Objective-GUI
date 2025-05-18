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
        return elementRectSetter.Set(rect);
    }
    public IDkReadOnlyRange<Vector2>? Range { get; set; }
}