using OG.Event.Abstraction;
using OG.TextController.Abstraction;
using OG.TextCursorController.Abstraction;

namespace OG.TextController;

public abstract class OgCharacterTextController(IOgTextCursorController textCursorController, bool multiLine) : IOgTextController
{
    protected string Value = string.Empty;
    public bool Multiline { get; set; } = multiLine;
    public IOgTextCursorController TextCursorController { get; } = textCursorController;

    public abstract string HandleKeyEvent(string text, IOgKeyDownEvent reason);

    public string HandleCharacter(string text, char character)
    {
        if(character == '\n' && !Multiline)
            return text;

        Value = text;
        ReplaceSelection(character.ToString());
        return Value;
    }

    protected virtual void DeleteSelectionIfNeeded()
    {
        int selectionPosition = TextCursorController.SelectionPosition.Get();
        int cursorPosition = TextCursorController.CursorPosition.Get();

        if(cursorPosition == selectionPosition) return;

        DeleteSelection(cursorPosition, selectionPosition);
    }

    protected virtual void DeleteSelection(int cursorPosition, int selectionPosition)
    {
        if(cursorPosition < selectionPosition) DeleteSelectionByCursor(cursorPosition, selectionPosition);
        else DeleteSelectionBySelection(cursorPosition, selectionPosition);
    }

    protected virtual void DeleteSelectionBySelection(int cursorPosition, int selectionPosition)
    {
        DeleteRange(selectionPosition, cursorPosition);
        _=TextCursorController.CursorPosition.Set(selectionPosition);
    }

    protected virtual void DeleteSelectionByCursor(int cursorPosition, int selectionPosition)
    {
        DeleteRange(cursorPosition, selectionPosition);
        _=TextCursorController.SelectionPosition.Set(selectionPosition);
    }

    protected virtual void DeleteRange(int from, int to) =>
        Value = Value.Remove(from, to - from);

    protected virtual void ReplaceSelection(string replace)
    {
        int cursorPosition = TextCursorController.CursorPosition.Get();
        DeleteSelectionIfNeeded();
        Value = Value.Insert(cursorPosition, replace);
        TextCursorController.ChangeCursorAndSelectionPositions(cursorPosition + replace.Length);
    }
}