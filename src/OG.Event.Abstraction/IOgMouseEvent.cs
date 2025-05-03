using OG.DataTypes.Point;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
namespace OG.Event.Abstraction;
public interface IOgMouseEvent : IOgInputEvent
{
    OgPoint LocalMousePosition { get; }
    void      Inline(OgPoint position);
}