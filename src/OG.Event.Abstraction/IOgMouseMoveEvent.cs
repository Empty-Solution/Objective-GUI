#region

using OG.DataTypes.Vector;

#endregion

namespace OG.Event.Abstraction;

public interface IOgMouseMoveEvent : IOgMouseEvent
{
    OgVector2 MouseMoveDelta { get; }
}