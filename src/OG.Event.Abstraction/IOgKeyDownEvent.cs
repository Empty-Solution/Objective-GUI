#region

using OG.DataTypes.KeyCode;

#endregion

namespace OG.Event.Abstraction;

public interface IOgKeyDownEvent : IOgKeyboardEvent
{
    EOgKeyCode KeyCode   { get; }
    char       Character { get; }
}