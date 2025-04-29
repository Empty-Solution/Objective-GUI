using OG.DataTypes.KeyCode;
using OG.Event.Abstraction;
using OG.TextCursorController.Abstraction;

namespace OG.Controller;

public abstract class OgTextController(IOgTextCursorController textCursorController, bool multiLine) : OgCharacterTextController(textCursorController, multiLine)
{
    protected abstract string SystemCopyBuffer { get; set; }

    public override string HandleKeyEvent(string text, IOgKeyDownEvent reason)
    {
        Value = text;
        EOgKeyCode keyCode = reason.KeyCode;

        switch(keyCode)
        {
            case EOgKeyCode.DELETE:
                if(reason.ControlModification)
                    DeleteWord(true);
                else
                    DeleteChar(true);
                break;
            case EOgKeyCode.BACKSPACE:
                if(reason.ControlModification)
                    DeleteWord(false);
                else
                    DeleteChar(false);
                break;
            case EOgKeyCode.LEFT_ARROW:
                if(reason.ControlModification)
                    MoveCursorWord(reason);
                else
                    MoveCursorChar(reason);
                break;
            case EOgKeyCode.RIGHT_ARROW:
                if(reason.ControlModification)
                    MoveCursorWord(reason, true);
                else
                    MoveCursorChar(reason, true);
                break;
            case EOgKeyCode.TAB:
                HandleTab();
                break;
            case EOgKeyCode.RETURN:
            case EOgKeyCode.KEYPAD_ENTER:
                HandleReturn();
                break;
            case EOgKeyCode.HOME:
                MoveCursorToStart(reason);
                break;
            case EOgKeyCode.END:
                MoveCursorToEnd(reason);
                break;
            case EOgKeyCode.A when reason.ControlModification:
                SelectAll();
                break;
            case EOgKeyCode.X when reason.ControlModification:
                Cut();
                break;
            case EOgKeyCode.C when reason.ControlModification:
                Copy();
                break;
            case EOgKeyCode.V when reason.ControlModification:
                Paste();
                break;
            default:
                return Value;
        }

        return Value;
    }


    protected virtual void HandleTab() =>
        Value = HandleCharacter(Value, '\t');

    protected virtual void HandleReturn()
    {
        if(!Multiline)
            return;

        Value = HandleCharacter(Value, '\n');
    }

    protected virtual void DeleteChar(bool forward)
    {
        int cursorPosition = TextCursorController.CursorPosition.Get();
        if(cursorPosition != TextCursorController.SelectionPosition.Get())
        {
            DeleteSelectionIfNeeded();
            return;
        }

        int target = Clamp(forward ? cursorPosition + 1 : cursorPosition - 1, 0, Value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(target, cursorPosition);
    }

    protected virtual void DeleteWord(bool forward)
    {
        string value = Value;
        if(string.IsNullOrEmpty(value)) return;
        int cursorPosition = TextCursorController.CursorPosition.Get();
        int wordBound = forward ? value.IndexOf(' ', Max(0, cursorPosition + 1)) : value.LastIndexOf(' ', Max(0, cursorPosition - 1));
        wordBound = Clamp(wordBound, 0, value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(wordBound, cursorPosition);
    }

    protected virtual void MoveCursorChar(IOgKeyboardEvent reason, bool forward = false)
    {
        if(reason.ShiftModification)
        {
            TextCursorController.SelectionPosition.Set(forward ? Min(Value.Length, TextCursorController.SelectionPosition.Get() + 1) : Max(0, TextCursorController.SelectionPosition.Get() - 1));
            return;
        }

        TextCursorController.ChangeCursorAndSelectionPositions(forward ? Min(Value.Length, TextCursorController.CursorPosition.Get() + 1) : Max(0, TextCursorController.CursorPosition.Get() - 1));
    }

    protected virtual void MoveCursorWord(IOgKeyboardEvent reason, bool forward = false)
    {
        int wordBound = forward ? Value.IndexOf(' ', TextCursorController.CursorPosition.Get() + 1) : Value.LastIndexOf(' ', Max(0, TextCursorController.CursorPosition.Get() - 1));
        wordBound = Clamp(wordBound, 0, Value.Length);
        MoveCursorTo(wordBound, reason);
    }

    protected virtual void MoveCursorToStart(IOgKeyboardEvent reason) => MoveCursorTo(0, reason);

    protected virtual void MoveCursorToEnd(IOgKeyboardEvent reason) => MoveCursorTo(Value.Length, reason);

    protected virtual void MoveCursorTo(int position, IOgKeyboardEvent reason)
    {
        if(reason.ShiftModification) TextCursorController.CursorPosition.Set(position);
        TextCursorController.SelectionPosition.Set(position);
    }

    protected virtual void SelectAll()
    {
        TextCursorController.CursorPosition.Set(0);
        TextCursorController.SelectionPosition.Set(Value.Length);
    }

    protected virtual void Cut()
    {
        if(TextCursorController.CursorPosition.Get() == TextCursorController.SelectionPosition.Get()) return;

        SystemCopyBuffer = GetSelectedText();
        DeleteSelectionIfNeeded();
    }

    protected virtual void Copy()
    {
        if(TextCursorController.CursorPosition.Get() == TextCursorController.SelectionPosition.Get()) return;

        SystemCopyBuffer = GetSelectedText();
    }

    protected virtual void Paste() =>
        ReplaceSelection(SystemCopyBuffer);

    protected virtual string GetSelectedText()
    {
        IOgTextCursorController controller = TextCursorController;
        int cursorPosition = controller.CursorPosition.Get();
        int selectionPosition = controller.SelectionPosition.Get();

        if(cursorPosition == selectionPosition) return string.Empty;

        int startIndex = Min(cursorPosition, selectionPosition);
        int length = Abs(cursorPosition - selectionPosition);

        return Value.Substring(startIndex, length);
    }

    protected virtual void DeleteRangeAndChangeCursorSelectionPositions(int from, int to)
    {
        if(to < from)
            (from, to) = (to, from);

        DeleteRange(from, to);
        TextCursorController.ChangeCursorAndSelectionPositions(from);
    }

    protected static float Clamp(float value, float min, float max) => value < min ? min : value > max ? max : value;
    protected static int Clamp(int value, int min, int max) => value < min ? min : value > max ? max : value;
    protected static int Min(int value, int min) => value < min ? min : value;
    protected static int Max(int value, int max) => value > max ? max : value;
    protected static int Abs(int value) => value < 0 ? -value : value;
}