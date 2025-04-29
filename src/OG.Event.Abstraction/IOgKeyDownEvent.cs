using OG.DataTypes.KeyCode;

namespace OG.Event.Abstraction;

public interface IOgKeyDownEvent : IOgKeyboardEvent
{
    EOgKeyCode KeyCode { get; }
    char Character { get; }
}