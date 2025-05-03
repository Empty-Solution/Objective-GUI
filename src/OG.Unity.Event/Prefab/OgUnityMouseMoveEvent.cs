using OG.DataTypes.Point;
using OG.DataTypes.Vector;
using OG.Event.Abstraction;
namespace OG.Unity.Event.Prefab;
public class OgUnityMouseMoveEvent : OgUnityMouseEvent, IOgMouseMoveEvent
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