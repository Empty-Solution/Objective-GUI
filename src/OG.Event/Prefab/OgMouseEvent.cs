using OG.DataTypes.Point;
using OG.Event.Abstraction;
namespace OG.Event.Prefab;
public class OgMouseEvent : OgInputEvent, IOgMouseEvent
{
    public OgPoint LocalMousePosition                    { get; private set; }
    public void    Inline(OgPoint              position) => LocalMousePosition -= position;
    public void    UpdateMousePosition(OgPoint mousePosition) => LocalMousePosition = mousePosition;
    protected virtual void OnMousePositionChanged(OgPoint mousePosition) => LocalMousePosition = mousePosition;
}