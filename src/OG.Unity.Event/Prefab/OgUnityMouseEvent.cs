#region

using OG.DataTypes.Vector;
using OG.Event.Abstraction;

#endregion

namespace OG.Unity.Event.Prefab;

public class OgUnityMouseEvent : OgUnityInputEvent, IOgMouseEvent
{
    public OgVector2 LocalMousePosition { get; set; }
}