using OG.DataTypes.Point;
using OG.DataTypes.Vector;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Unity.Event.Prefab;
public class OgUnityMouseEvent : OgUnityInputEvent, IOgMouseEvent
{
    public  OgPoint   LocalMousePosition       { get; private set; }
    public  void      Inline(OgPoint position) => LocalMousePosition -= position;
    public void Fill(UnityEngine.Event ueEvent)
    {
        Vector2 sourceMousePosition = ueEvent.mousePosition;
        OgPoint mousePosition       = new((int)sourceMousePosition.x, (int)sourceMousePosition.y);
        OnMousePositionChanged(mousePosition);
    }
    protected virtual void OnMousePositionChanged(OgPoint mousePosition) => LocalMousePosition = mousePosition;
}