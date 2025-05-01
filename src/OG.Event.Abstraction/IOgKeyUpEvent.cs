using OG.DataTypes.KeyCode;
namespace OG.Event.Abstraction;
public interface IOgKeyUpEvent : IOgKeyboardEvent
{
    EOgKeyCode KeyCode   { get; }
    char       Character { get; }
}