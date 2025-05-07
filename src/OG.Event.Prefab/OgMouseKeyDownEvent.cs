using OG.Event.Prefab.Abstraction;
namespace OG.Event.Prefab;
public class OgMouseKeyDownEvent(UnityEngine.Event source) : OgMouseKeyEvent(source), IOgMouseKeyDownEvent;