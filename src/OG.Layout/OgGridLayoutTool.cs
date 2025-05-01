using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;

namespace OG.Layout;

public class OgGridLayoutTool<TElement>(int spacing, OgVector2 gridSize) : OgLayoutTool<TElement>(spacing) where TElement : IOgElement
{
    private OgVector2 m_GridPosition;
    private int       m_MaxHeight;

    public override void ResetLayout()
    {
        base.ResetLayout();
        m_GridPosition = new();
        m_MaxHeight    = 0;
    }

    public override OgRectangle GetRectangle(OgRectangle elementRect, OgRectangle lastRect, int spacing)
    {
        if(m_GridPosition.Y > gridSize.Y) ResetLayout();

        if(m_GridPosition.X > gridSize.X)
        {
            m_GridPosition.X = 0;
            m_GridPosition.Y++;

            return new(0, lastRect.Y + m_MaxHeight + spacing, elementRect.Width, elementRect.Height);
        }

        int         itemHeight = elementRect.Height;
        OgRectangle rect       = new(lastRect.XMax + spacing, lastRect.Y, elementRect.Width, itemHeight);

        UpdateMaxHeightIfNeeded(itemHeight);

        m_GridPosition.X++;
        return rect;
    }

    private void UpdateMaxHeightIfNeeded(int newHeight)
    {
        if(newHeight <= m_MaxHeight) return;
        m_MaxHeight = newHeight;
    }
}