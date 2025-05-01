using OG.DataTypes.KeyboardModifier;
using OG.Event.Abstraction;
namespace OG.Unity.Event.Prefab;
public class OgUnityKeyboardEvent : OgUnityInputEvent, IOgKeyboardEvent
{
    public EOgKeyboardModifier Modifier { get; set; }
}