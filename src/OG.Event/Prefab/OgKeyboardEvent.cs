using OG.DataTypes.KeyboardModifier;
using OG.Event.Abstraction;
namespace OG.Event.Prefab;
public class OgKeyboardEvent : OgInputEvent, IOgKeyboardEvent
{
    public EOgKeyboardModifier Modifier { get; set; }
}