using OG.DataTypes.Point;
using OG.DataTypes.Vector;
using OG.Event.Abstraction;
namespace OG.Event.Prefab;
public class OgMouseMoveEvent : OgMouseEvent, IOgMouseMoveEvent
{
    private OgPoint   m_LastMousePosition = new();
    public  OgVector2 MouseMoveDelta { get; private set; }
    protected override void OnMousePositionChanged(OgPoint mousePosition)
    {
        base.OnMousePositionChanged(mousePosition);
        OgPoint deltaPoint = mousePosition - m_LastMousePosition;
        MouseMoveDelta      = new(deltaPoint.X, deltaPoint.Y);
        m_LastMousePosition = mousePosition;
    }
}