#region

using OG.Event.Abstraction;
using OG.TextCursorController.Abstraction;

#endregion

namespace OG.TextController.Abstraction;

public interface IOgTextController
{
    IOgTextCursorController TextCursorController { get; }
    string HandleKeyEvent(string text, IOgKeyDownEvent reason);
    string HandleCharacter(string text, char character);
}