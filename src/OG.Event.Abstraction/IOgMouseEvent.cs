#region

using OG.DataTypes.Vector;

#endregion

namespace OG.Event.Abstraction;

public interface IOgMouseEvent : IOgInputEvent
{
    OgVector2 LocalMousePosition { get; }
}