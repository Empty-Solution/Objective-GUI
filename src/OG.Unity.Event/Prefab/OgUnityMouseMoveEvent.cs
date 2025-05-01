
using OG.DataTypes.Vector;
using OG.Event.Abstraction;

namespace OG.Unity.Event.Prefab;

public class OgUnityMouseMoveEvent : OgUnityMouseEvent, IOgMouseMoveEvent
{
    public OgVector2 MouseMoveDelta { get; set; }
}