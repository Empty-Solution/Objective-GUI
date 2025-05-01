using OG.DataTypes.KeyboardModifier;
namespace OG.Event.Abstraction;
public interface IOgKeyboardEvent : IOgInputEvent
{
    EOgKeyboardModifier Modifier { get; }
}