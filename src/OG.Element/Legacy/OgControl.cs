using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public abstract class OgControl<TElement, TScope>(string name, TScope scope, IOgTransform transform)
    : OgContainer<TElement, TScope>(name, scope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override void InternalOnGUI(OgEvent reason)
    {
        base.InternalOnGUI(reason);
        DoElement(reason, Transform.LocalRect);
    }

    protected abstract void DoElement(OgEvent reason, Rect rect);
}