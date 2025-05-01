
using OG.DataTypes.KeyCode;
using OG.Event.Abstraction;

namespace OG.Unity.Event.Prefab;

public class OgUnityKeyUpEvent : OgUnityKeyboardEvent, IOgKeyUpEvent
{
    public EOgKeyCode KeyCode { get; set; }
    public char Character { get; set; }
}