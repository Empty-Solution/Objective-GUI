using OG.Common.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element;

public class OgTextEditor(IOgTextCursorController textCursorController, bool multiLine) : OgCharacterTextEditor(textCursorController, multiLine)
{
    public override string HandleKeyEvent(OgEvent reason, string text, Rect rect, out bool handled)
    {
        Value = text;
        KeyCode keyCode = reason.KeyCode;

        switch(keyCode)
        {
            case KeyCode.Delete:
                if(reason.ControlModification)
                    DeleteWord(reason, true, rect);
                else
                    DeleteChar(reason, true, rect);
                break;
            case KeyCode.Backspace:
                if(reason.ControlModification)
                    DeleteWord(reason, false, rect);
                else
                    DeleteChar(reason, false, rect);
                break;
            case KeyCode.LeftArrow:
                if(reason.ControlModification)
                    MoveCursorWord(reason, false, rect);
                else
                    MoveCursorChar(reason, false, rect);
                break;
            case KeyCode.RightArrow:
                if(reason.ControlModification)
                    MoveCursorWord(reason, true, rect);
                else
                    MoveCursorChar(reason, true, rect);
                break;
            case KeyCode.Tab:
                HandleTab(reason, rect);
                break;
            case KeyCode.Return:
            case KeyCode.KeypadEnter:
                HandleReturn(reason, rect);
                break;
            case KeyCode.Home:
                MoveCursorToStart(reason, rect);
                break;
            case KeyCode.End:
                MoveCursorToEnd(reason, rect);
                break;
            case KeyCode.A when reason.ControlModification:
                SelectAll(reason, rect);
                break;
            case KeyCode.X when reason.ControlModification:
                Cut(reason, rect);
                break;
            case KeyCode.C when reason.ControlModification:
                Copy();
                break;
            case KeyCode.V when reason.ControlModification:
                Paste(reason, rect);
                break;
            default:
                handled = false;
                return Value;
        }

        handled = true;
        return Value;
    }

    protected virtual void HandleTab(OgEvent reason, Rect rect) =>
        Value = HandleCharacter(reason, Value, '\t', rect);

    protected virtual void HandleReturn(OgEvent reason, Rect rect)
    {
        if(!Multiline)
            return;

        Value = HandleCharacter(reason, Value, '\n', rect);
    }

    protected virtual void DeleteChar(OgEvent reason, bool forward, Rect rect)
    {
        IOgTextCursorController controller = TextCursorController;
        int cursorPosition = controller.CursorPosition;

        if(cursorPosition != controller.SelectionPosition)
        {
            DeleteSelectionIfNeeded(reason, rect);
            return;
        }

        int target = Mathf.Clamp(forward ? cursorPosition + 1 : cursorPosition - 1, 0, Value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(target, cursorPosition, reason, rect);
    }

    protected virtual void DeleteWord(OgEvent reason, bool forward, Rect rect)
    {
        string value = Value;
        if(string.IsNullOrEmpty(value)) return;
        int cursorPosition = TextCursorController.CursorPosition;
        int wordBound = forward ? value.IndexOf(' ', Mathf.Max(0, cursorPosition + 1)) : value.LastIndexOf(' ', Mathf.Max(0, cursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(wordBound, cursorPosition, reason, rect);
    }

    protected virtual void MoveCursorChar(OgEvent reason, bool forward, Rect rect)
    {
        IOgTextCursorController controller = TextCursorController;
        if(reason.ShiftModification)
        {
            controller.ChangeSelectionPosition(reason, forward ? Mathf.Min(Value.Length, controller.SelectionPosition + 1) : Mathf.Max(0, controller.SelectionPosition - 1), Value, rect);
            return;
        }

        controller.ChangeCursorAndSelectionPositions(reason, forward ? Mathf.Min(Value.Length, controller.CursorPosition + 1) : Mathf.Max(0, controller.CursorPosition - 1), Value, rect);
    }

    protected virtual void MoveCursorWord(OgEvent reason, bool forward, Rect rect)
    {
        IOgTextCursorController controller = TextCursorController;
        int wordBound = forward ? Value.IndexOf(' ', controller.CursorPosition + 1) : Value.LastIndexOf(' ', Mathf.Max(0, controller.CursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, Value.Length);
        MoveCursorTo(wordBound, reason, rect);
    }

    protected virtual void MoveCursorToStart(OgEvent reason, Rect rect) => MoveCursorTo(0, reason, rect);

    protected virtual void MoveCursorToEnd(OgEvent reason, Rect rect) => MoveCursorTo(Value.Length, reason, rect);

    protected virtual void MoveCursorTo(int position, OgEvent reason, Rect rect)
    {
        IOgTextCursorController controller = TextCursorController;
        if(reason.ShiftModification) controller.ChangeCursorPosition(reason, position, Value, rect);
        controller.ChangeSelectionPosition(reason, position, Value, rect);
        ;
    }

    protected virtual void SelectAll(OgEvent reason, Rect rect)
    {
        IOgTextCursorController controller = TextCursorController;
        controller.ChangeCursorPosition(reason, 0, Value, rect);
        controller.ChangeSelectionPosition(reason, Value.Length, Value, rect);
    }

    protected virtual void Cut(OgEvent reason, Rect rect)
    {
        IOgTextCursorController controller = TextCursorController;
        if(controller.CursorPosition == controller.SelectionPosition) return;

        GUIUtility.systemCopyBuffer = GetSelectedText();
        DeleteSelectionIfNeeded(reason, rect);
    }

    protected virtual void Copy()
    {
        IOgTextCursorController controller = TextCursorController;
        if(controller.CursorPosition == controller.SelectionPosition) return;

        GUIUtility.systemCopyBuffer = GetSelectedText();
    }

    protected virtual void Paste(OgEvent reason, Rect rect) =>
        ReplaceSelection(GUIUtility.systemCopyBuffer, reason, rect);

    protected virtual string GetSelectedText()
    {
        IOgTextCursorController controller = TextCursorController;
        int cursorPosition = controller.CursorPosition;
        int selectionPosition = controller.SelectionPosition;

        if(cursorPosition == selectionPosition) return string.Empty;

        int startIndex = Mathf.Min(cursorPosition, selectionPosition);
        int length = Mathf.Abs(cursorPosition - selectionPosition);

        return Value.Substring(startIndex, length);
    }

    protected virtual void DeleteRangeAndChangeCursorSelectionPositions(int from, int to, OgEvent reason, Rect rect)
    {
        if(to < from)
            (from, to) = (to, from);

        DeleteRange(from, to);
        TextCursorController.ChangeCursorAndSelectionPositions(reason, from, Value, rect);
    }
}