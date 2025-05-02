using DK.Property.Abstraction.Generic;
using OG.DataTypes.KeyboardModifier;
using OG.DataTypes.KeyCode;
using OG.Event.Abstraction;
using OG.TextCursorController.Abstraction;
namespace OG.TextController;
public class OgTextController(IOgTextCursorController textCursorController, bool multiLine) : OgCharacterTextController(textCursorController, multiLine)
{
    protected IDkProperty<string> SystemCopyBuffer { get; set; }
    public override string HandleKeyEvent(string text, IOgKeyDownEvent reason)
    {
        m_Value = text;
        EOgKeyCode keyCode = reason.KeyCode;
        switch(keyCode)
        {
            case EOgKeyCode.DELETE:
                if(reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL))
                    DeleteWord(true);
                else
                    DeleteChar(true);
                break;
            case EOgKeyCode.BACKSPACE:
                if(reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL))
                    DeleteWord(false);
                else
                    DeleteChar(false);
                break;
            case EOgKeyCode.LEFT_ARROW:
                if(reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL))
                    MoveCursorWord(reason);
                else
                    MoveCursorChar(reason);
                break;
            case EOgKeyCode.RIGHT_ARROW:
                if(reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL))
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
            case EOgKeyCode.A when reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL):
                SelectAll();
                break;
            case EOgKeyCode.X when reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL):
                Cut();
                break;
            case EOgKeyCode.C when reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL):
                Copy();
                break;
            case EOgKeyCode.V when reason.Modifier.HasFlag(EOgKeyboardModifier.CONTROL):
                Paste();
                break;
        }
        return m_Value;
    }
    protected virtual void HandleTab() => m_Value = HandleCharacter(m_Value, '\t');
    protected virtual void HandleReturn()
    {
        if(!Multiline) return;
        m_Value = HandleCharacter(m_Value, '\n');
    }
    protected virtual void DeleteChar(bool forward)
    {
        int cursorPosition = TextCursorController.CursorPosition.Get();
        if(cursorPosition != TextCursorController.SelectionPosition.Get())
        {
            DeleteSelectionIfNeeded();
            return;
        }
        int target = Clamp(forward ? cursorPosition + 1 : cursorPosition - 1, 0, m_Value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(target, cursorPosition);
    }
    protected virtual void DeleteWord(bool forward)
    {
        string value = m_Value;
        if(string.IsNullOrEmpty(value)) return;
        int cursorPosition = TextCursorController.CursorPosition.Get();
        int wordBound      = forward ? value.IndexOf(' ', Max(0, cursorPosition + 1)) : value.LastIndexOf(' ', Max(0, cursorPosition - 1));
        wordBound = Clamp(wordBound, 0, value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(wordBound, cursorPosition);
    }
    protected virtual void MoveCursorChar(IOgKeyboardEvent reason, bool forward = false)
    {
        if(reason.Modifier.HasFlag(EOgKeyboardModifier.SHIFT))
        {
            _ = TextCursorController.SelectionPosition.Set(forward ? Min(m_Value.Length, TextCursorController.SelectionPosition.Get() + 1)
                                                               : Max(0, TextCursorController.SelectionPosition.Get() - 1));
            return;
        }
        TextCursorController.ChangeCursorAndSelectionPositions(forward ? Min(m_Value.Length, TextCursorController.CursorPosition.Get() + 1)
                                                                   : Max(0, TextCursorController.CursorPosition.Get() - 1));
    }
    protected virtual void MoveCursorWord(IOgKeyboardEvent reason, bool forward = false)
    {
        int wordBound = forward ? m_Value.IndexOf(' ', TextCursorController.CursorPosition.Get() + 1)
                            : m_Value.LastIndexOf(' ', Max(0, TextCursorController.CursorPosition.Get() - 1));
        wordBound = Clamp(wordBound, 0, m_Value.Length);
        MoveCursorTo(wordBound, reason);
    }
    protected virtual void MoveCursorToStart(IOgKeyboardEvent reason) => MoveCursorTo(0, reason);
    protected virtual void MoveCursorToEnd(IOgKeyboardEvent reason) => MoveCursorTo(m_Value.Length, reason);
    protected virtual void MoveCursorTo(int position, IOgKeyboardEvent reason)
    {
        if(reason.Modifier.HasFlag(EOgKeyboardModifier.SHIFT)) _ = TextCursorController.CursorPosition.Set(position);
        _ = TextCursorController.SelectionPosition.Set(position);
    }
    protected virtual void SelectAll()
    {
        _ = TextCursorController.CursorPosition.Set(0);
        _ = TextCursorController.SelectionPosition.Set(m_Value.Length);
    }
    protected virtual void Cut()
    {
        if(TextCursorController.CursorPosition.Get() == TextCursorController.SelectionPosition.Get()) return;
        SystemCopyBuffer.Set(GetSelectedText());
        DeleteSelectionIfNeeded();
    }
    protected virtual void Copy()
    {
        if(TextCursorController.CursorPosition.Get() == TextCursorController.SelectionPosition.Get()) return;
        SystemCopyBuffer.Set(GetSelectedText());
    }
    protected virtual void Paste() => ReplaceSelection(SystemCopyBuffer.Get());
    protected virtual string GetSelectedText()
    {
        IOgTextCursorController controller        = TextCursorController;
        int                     cursorPosition    = controller.CursorPosition.Get();
        int                     selectionPosition = controller.SelectionPosition.Get();
        if(cursorPosition == selectionPosition) return string.Empty;
        int startIndex = Min(cursorPosition, selectionPosition);
        int length     = Abs(cursorPosition - selectionPosition);
        return m_Value.Substring(startIndex, length);
    }
    protected virtual void DeleteRangeAndChangeCursorSelectionPositions(int from, int to)
    {
        if(to < from) (from, to) = (to, from);
        DeleteRange(from, to);
        TextCursorController.ChangeCursorAndSelectionPositions(from);
    }
    protected static int Clamp(int value, int min, int max) => value < min ? min : value > max ? max : value;
    protected static int Min(int value, int min) => value < min ? min : value;
    protected static int Max(int value, int max) => value > max ? max : value;
    protected static int Abs(int value) => value < 0 ? -value : value;
}