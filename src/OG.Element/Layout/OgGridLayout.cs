using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Layout;

public class OgGridLayout<TElement>(float space, Vector2Int gridSize) : OgLayout<TElement>(space) where TElement : IOgElement
{
    private float m_MaxHeight;
    private Vector2Int m_GridPosition;

    protected override void ResetLayout()
    {
        base.ResetLayout();
        m_GridPosition = Vector2Int.zero;
        m_MaxHeight = 0.0f;
    }

    protected override Rect GetNextRect(Rect itemRect, Rect lastRect, float space)
    {
        if(m_GridPosition.y > gridSize.y) ResetLayout();

        if(m_GridPosition.x > gridSize.x)
        {
            m_GridPosition.x = 0;
            m_GridPosition.y++;

            return new(0.0f, lastRect.y + m_MaxHeight + space, itemRect.width, itemRect.height);
        }

        float itemHeight = itemRect.height;
        Rect rect = new(lastRect.xMax + space, lastRect.y, itemRect.width, itemHeight);

        UpdateMaxHeightIfNeeded(itemHeight);

        m_GridPosition.x++;
        return rect;
    }

    private void UpdateMaxHeightIfNeeded(float nHeigth)
    {
        if(nHeigth <= m_MaxHeight) return;
        m_MaxHeight = nHeigth;
    }
}