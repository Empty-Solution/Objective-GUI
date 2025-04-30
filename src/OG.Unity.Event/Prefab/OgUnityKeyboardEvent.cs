#region

using OG.Event.Abstraction;

#endregion

namespace OG.Unity.Event.Prefab;

public class OgUnityKeyboardEvent : OgUnityInputEvent, IOgKeyboardEvent
{
    public bool ShiftModification   { get; set; }
    public bool ControlModification { get; set; }
}