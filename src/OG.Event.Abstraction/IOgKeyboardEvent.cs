using OG.DataTypes.KeyCode;

namespace OG.Event.Abstraction;

public interface IOgKeyboardEvent : IOgInputEvent
{
    bool ShiftModification { get; }
    bool ControlModification { get; }
}