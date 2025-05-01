using OG.DataTypes.Vector;

namespace OG.Event.Abstraction;

public interface IOgMouseEvent : IOgInputEvent
{
    OgVector2 LocalMousePosition { get; set; }
}