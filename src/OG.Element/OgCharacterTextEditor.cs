using OG.Common.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element;

public abstract class OgCharacterTextEditor(IOgTextCursorController textCursorController, bool multiLine) : IOgTextEditor
{
    protected string Value = string.Empty;
    public bool Multiline { get; set; } = multiLine;
    public IOgTextCursorController TextCursorController { get; } = textCursorController;

    public abstract string HandleKeyEvent(OgEvent reason, string text, Rect rect, out bool handled);

    public string HandleCharacter(OgEvent reason, string text, char character, Rect rect)
    {
        if(character == '\n' && !Multiline)
            return text;

        Value = text;
        ReplaceSelection(character.ToString(), reason, rect);
        return Value;
    }

    protected virtual void DeleteSelectionIfNeeded(OgEvent reason, Rect rect)
    {
        int selectionPosition = TextCursorController.SelectionPosition;
        int cursorPosition = TextCursorController.CursorPosition;

        if(cursorPosition == selectionPosition) return;

        DeleteSelection(cursorPosition, selectionPosition, reason, rect);
    }

    protected virtual void DeleteSelection(int cursorPosition, int selectionPosition, OgEvent reason, Rect rect)
    {
        if(cursorPosition < selectionPosition) DeleteSelectionByCursor(cursorPosition, selectionPosition, reason, rect);
        else DeleteSelectionBySelection(cursorPosition, selectionPosition, reason, rect);
    }

    protected virtual void DeleteSelectionBySelection(int cursorPosition, int selectionPosition, OgEvent reason, Rect rect)
    {
        DeleteRange(selectionPosition, cursorPosition);
        TextCursorController.ChangeCursorPosition(reason, selectionPosition, Value, rect);
    }

    protected virtual void DeleteSelectionByCursor(int cursorPosition, int selectionPosition, OgEvent reason, Rect rect)
    {
        DeleteRange(cursorPosition, selectionPosition);
        TextCursorController.ChangeSelectionPosition(reason, cursorPosition, Value, rect);
    }

    protected virtual void DeleteRange(int from, int to) =>
        Value = Value.Remove(from, to - from);

    protected virtual void ReplaceSelection(string replace, OgEvent reason, Rect rect)
    {
        int cursorPosition = TextCursorController.CursorPosition;
        DeleteSelectionIfNeeded(reason, rect);
        Value = Value.Insert(cursorPosition, replace);
        TextCursorController.ChangeCursorAndSelectionPositions(reason, cursorPosition + replace.Length, Value, rect);
    }
}