#region

using OG.DataTypes.Vector;

#endregion

namespace OG.Event.Abstraction;

public interface IOgMouseScrollEvent : IOgMouseEvent
{
    OgVector2 ScrollDelta { get; }
}