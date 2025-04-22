using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgScroll<TElement, TScope>(string name, TScope scope, IOgTransform transform)
    : OgValueView<TElement, TScope, Vector2>(name, scope, transform, Vector2.zero) where TElement : IOgElement where TScope : IOgClipTransformScope
{
    protected override void HandleMouseScroll(OgEvent reason)
    {
        if(!IsHovered) return;
        ChangeScrollPosition(reason.ScrollDelta, reason);
    }

    protected virtual void ChangeScrollPosition(Vector2 scrollDelta, OgEvent reason)
    {
        Vector2 position = Value + scrollDelta;
        ChangeValue(position, reason);
        Scope.ScrollPosition = position;
    }
}