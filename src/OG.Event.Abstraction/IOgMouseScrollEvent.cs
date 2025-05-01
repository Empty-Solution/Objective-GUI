
using OG.DataTypes.Vector;

namespace OG.Event.Abstraction;

public interface IOgMouseScrollEvent : IOgMouseEvent
{
    OgVector2 ScrollDelta { get; }
}