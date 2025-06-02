using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgMouseMoveEventPipe : OgMouseEventPipe<IOgMouseMoveEvent>
{
    private Vector2 m_LastMousePosition;
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.Layout;
    protected override IOgMouseMoveEvent InternalGetEvent(UnityEngine.Event sourceEvent)
    {
        Vector2          mousePosition  = sourceEvent.mousePosition;
        Vector2          mouseDelta     = mousePosition - m_LastMousePosition;
        OgMouseMoveEvent mouseMoveEvent = new(sourceEvent, mouseDelta);
        m_LastMousePosition = mousePosition;
        return mouseMoveEvent;
    }
}