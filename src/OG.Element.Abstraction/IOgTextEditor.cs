using OG.Common.Abstraction;
using UnityEngine;

namespace OG.Element.Abstraction;

public interface IOgTextEditor
{
    IOgTextCursorController TextCursorController { get; }
    string HandleKeyEvent(OgEvent reason, string text, Rect rect, out bool handled);
    string HandleCharacter(OgEvent reason, string text, char character, Rect rect);
}