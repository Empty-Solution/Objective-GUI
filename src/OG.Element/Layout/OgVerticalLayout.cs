using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Layout;

public class OgVerticalLayout<TElement>(float space) : OgLayout<TElement>(space) where TElement : IOgElement
{
    protected override Rect GetNextRect(Rect itemRect, Rect lastRect, float space) => new(lastRect.x, lastRect.yMax + space, itemRect.width, itemRect.height);
}