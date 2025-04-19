using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element;

public class OgVerticalLayout<TElement>(Vector2 gap, Vector2 padding) : OgLayout<TElement> where TElement : IOgElement
{
    public override void ProcessItem(TElement element)
    {
        Rect rect = element.Transform.LocalRect;
        rect.position = new Vector2(rect.position.x, m_LastRect.yMax) + (m_LastRect == Rect.zero ? padding : gap);
        element.Transform.LocalRect = rect;
        m_LastRect = rect;
    }
}