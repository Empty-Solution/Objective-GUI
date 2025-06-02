using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgToggle<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<bool> value)
    : OgInteractableValueElement<TElement, bool>(name, provider, rectGetter, value), IOgToggle<TElement> where TElement : IOgElement
{
    protected override bool EndControl(IOgMouseKeyUpEvent reason) => base.EndControl(reason) | Value.Set(!Value.Get());
}