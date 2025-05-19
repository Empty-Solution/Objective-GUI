using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
namespace OG.TextController.Abstraction;
public interface IOgTextController
{
    IOgTextCursorController TextCursorController { get; }
    string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context);
    string HandleCharacter(string text, char character, IOgGraphicsContext context);
}