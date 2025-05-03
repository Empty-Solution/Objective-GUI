using OG.DataTypes.Vector;
using OG.DataTypes.Vector.Float;
namespace OG.Event.Abstraction;
public interface IOgMouseScrollEvent : IOgMouseEvent
{
    OgVector2 ScrollDelta { get; }
}