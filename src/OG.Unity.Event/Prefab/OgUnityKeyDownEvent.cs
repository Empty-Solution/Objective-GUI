using OG.DataTypes.KeyCode;
using OG.Event.Abstraction;

namespace OG.Unity.Event.Prefab;

public class OgUnityKeyDownEvent : OgUnityKeyboardEvent, IOgKeyDownEvent
{
    public EOgKeyCode KeyCode { get; set; }

    public char Character { get; set; }
}