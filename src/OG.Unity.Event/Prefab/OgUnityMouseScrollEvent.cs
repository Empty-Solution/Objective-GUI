using OG.DataTypes.Vector;
using OG.DataTypes.Vector.Float;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Unity.Event.Prefab;
public class OgUnityMouseScrollEvent : OgUnityMouseEvent, IOgMouseScrollEvent
{
    public OgVector2 ScrollDelta { get; private set; }
    public void Fill(UnityEngine.Event ueEvent)
    {
        base.Fill(ueEvent);
        Vector2 sourceDelta = ueEvent.delta;
        ScrollDelta = new((int)sourceDelta.x, (int)sourceDelta.y);
    }
}