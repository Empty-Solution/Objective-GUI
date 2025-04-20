using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element;

public class OgGridLayout<TElement>(Vector2 gap, Vector2 padding, int columns) : OgLayout<TElement> where TElement : IOgElement
{
    protected int m_CurrentColumn;
    protected int m_CurrentRow;

    public override void Open()
    {
        base.Open();
        ResetLayout();
    }

    public override void Close()
    {
        base.Close();
        ResetLayout();
    }

    protected override void ResetLayout()
    {
        base.ResetLayout();
        m_CurrentColumn = 0;
        m_CurrentRow = 0;
    }

    public override void ProcessItem(TElement element)
    {
        Rect rect = element.Transform.LocalRect;
        rect.position = new((m_CurrentColumn * (rect.size.x + gap.x)) + padding.x,
            (m_CurrentRow * (rect.size.y + gap.y)) + padding.y);
        element.Transform.LocalRect = rect;

        m_CurrentColumn++;
        if(m_CurrentColumn < columns)
            return;
        m_CurrentColumn = 0;
        m_CurrentRow++;
    }
}