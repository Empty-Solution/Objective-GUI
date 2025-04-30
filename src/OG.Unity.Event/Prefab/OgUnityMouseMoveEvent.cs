#region

using OG.DataTypes.Vector;
using OG.Event.Abstraction;

#endregion

namespace OG.Unity.Event.Prefab;

public class OgUnityMouseMoveEvent : OgUnityMouseEvent, IOgMouseMoveEvent
{
    public OgVector2 MouseMoveDelta { get; set; }
}