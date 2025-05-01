
using OG.DataTypes.Vector;
using OG.Event.Abstraction;

namespace OG.Unity.Event.Prefab;

public class OgUnityMouseScrollEvent : OgUnityMouseEvent, IOgMouseScrollEvent
{
    public OgVector2 ScrollDelta { get; set; }
}