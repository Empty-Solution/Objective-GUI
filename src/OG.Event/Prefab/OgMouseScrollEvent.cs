using OG.DataTypes.Vector;
using OG.Event.Abstraction;
namespace OG.Event.Prefab;
public class OgMouseScrollEvent : OgMouseEvent, IOgMouseScrollEvent
{
    public OgVector2 ScrollDelta { get; private set; }
    public void UpdateScrollDelta(OgVector2 delta) => ScrollDelta = delta;
}