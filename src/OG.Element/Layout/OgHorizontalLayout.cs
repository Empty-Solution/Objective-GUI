using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Layout;

public class OgHorizontalLayout<TElement>(float space) : OgLayout<TElement>(space) where TElement : IOgElement
{
    protected override Rect GetNextRect(Rect itemRect, Rect lastRect, float space) => new(lastRect.xMax + space, lastRect.y, itemRect.width, itemRect.height);
}