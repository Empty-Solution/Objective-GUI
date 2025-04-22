using DK.Scoping;
using OG.Common.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Layout;

public abstract class OgLayout<TElement>(float space) : DkScope, IOgLayout<TElement> where TElement : IOgElement
{
    protected Rect m_LastRect;

    public void ProcessItem(TElement element)
    {
        IOgTransform transform = element.Transform;
        Rect nextRect = GetNextRect(transform.LocalRect, m_LastRect, space);
        m_LastRect = nextRect;
        transform.LocalRect = nextRect;
    }

    protected virtual void ResetLayout() => ResetLastRect();

    protected void ResetLastRect() => m_LastRect = Rect.zero;

    protected abstract Rect GetNextRect(Rect itemRect, Rect lastRect, float space);

    protected override void OnOpened() => ResetLayout();

    protected override void OnClosed() => ResetLayout();
}