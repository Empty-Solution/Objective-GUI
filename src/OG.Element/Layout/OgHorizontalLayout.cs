using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element;

public class OgHorizontalLayout<TElement>(Vector2 gap, Vector2 padding) : OgLayout<TElement> where TElement : IOgElement
{
    public override void ProcessItem(TElement element)
    {
        Rect rect = element.Transform.LocalRect;
        rect.position = new Vector2(m_LastRect.xMax, rect.position.y) + (m_LastRect == Rect.zero ? padding : gap);
        element.Transform.LocalRect = rect;
        m_LastRect = rect;
    }
}