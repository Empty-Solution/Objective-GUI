using OG.DataTypes.Vector;
namespace OG.Event.Abstraction;
public interface IOgMouseMoveEvent : IOgMouseEvent
{
    OgVector2 MouseMoveDelta { get; }
}