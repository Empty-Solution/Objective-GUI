using OG.DataTypes.Point;
namespace OG.Event.Abstraction;
public interface IOgMouseEvent : IOgInputEvent
{
    OgPoint LocalMousePosition { get; }
    void    Inline(OgPoint position);
}