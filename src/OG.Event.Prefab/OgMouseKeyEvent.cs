using OG.Event.Prefab.Abstraction;
namespace OG.Event.Prefab;
public abstract class OgMouseKeyEvent(UnityEngine.Event source) : OgMouseEvent(source), IOgMouseKeyEvent
{
    public int Key { get; set; } = source.button;
}