using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Observing;
public class EhTabObserver(List<EhBaseTabObserver> observers, IOgContainer<IOgElement> source, IOgContainer<IOgElement> target, float thumbSize,
    OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter)
    : EhBaseTabObserver(observers, source, target, thumbSize, separatorSelectorGetter)
{
    protected override Rect GetRect(Rect rect, bool state, float size)
    {
        rect.y      = RectGetter!.Get().y;
        rect.height = state ? size : 0;
        return rect;
    }
}