#region

using OG.DataTypes.KeyCode;

#endregion

namespace OG.Event.Abstraction;

public interface IOgKeyUpEvent : IOgKeyboardEvent
{
    EOgKeyCode KeyCode   { get; }
    char       Character { get; }
}