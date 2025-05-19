using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
using UnityEngine;
namespace OG.TextController;
public class OgTextController(IOgTextCursorController textCursorController, bool multiLine) : OgCharacterTextController(textCursorController, multiLine)
{
    public override string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context)
    {
        m_Value = text;
        KeyCode keyCode    = reason.KeyCode;
        bool    hasControl = reason.Modifiers.HasFlag(EventModifiers.Control);
        switch(keyCode)
        {
            case KeyCode.Delete:
                if(hasControl)
                    DeleteWord(true, context);
                else
                    DeleteChar(true, context);
                break;
            case KeyCode.Backspace:
                if(hasControl)
                    DeleteWord(false, context);
                else
                    DeleteChar(false, context);
                break;
            case KeyCode.LeftArrow:
                if(hasControl)
                    MoveCursorWord(reason, context);
                else
                    MoveCursorChar(reason, context);
                break;
            case KeyCode.RightArrow:
                if(hasControl)
                    MoveCursorWord(reason, context, true);
                else
                    MoveCursorChar(reason, context, true);
                break;
            case KeyCode.Tab:
                HandleTab(context);
                break;
            case KeyCode.Return:
            case KeyCode.KeypadEnter:
                HandleReturn(context);
                break;
            case KeyCode.Home:
                MoveCursorToStart(reason, context);
                break;
            case KeyCode.End:
                MoveCursorToEnd(reason, context);
                break;
            case KeyCode.A when hasControl:
                SelectAll(context);
                break;
            case KeyCode.X when hasControl:
                Cut(context);
                break;
            case KeyCode.C when hasControl:
                Copy(context);
                break;
            case KeyCode.V when hasControl:
                Paste(context);
                break;
        }
        return m_Value;
    }
    private void HandleTab(IOgGraphicsContext context) => m_Value = HandleCharacter(m_Value, '\t', context);
    private void HandleReturn(IOgGraphicsContext context)
    {
        if(!Multiline) return;
        m_Value = HandleCharacter(m_Value, '\n', context);
    }
    private void DeleteChar(bool forward, IOgGraphicsContext context)
    {
        int cursorPosition = TextCursorController.CursorPosition;
        if(cursorPosition != TextCursorController.SelectionPosition)
        {
            DeleteSelectionIfNeeded(context);
            return;
        }
        int target = Mathf.Clamp(forward ? cursorPosition + 1 : cursorPosition - 1, 0, m_Value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(target, cursorPosition, context);
    }
    private void DeleteWord(bool forward, IOgGraphicsContext context)
    {
        string value = m_Value;
        if(string.IsNullOrEmpty(value)) return;
        int cursorPosition = TextCursorController.CursorPosition;
        int wordBound      = forward ? value.IndexOf(' ', Mathf.Max(0, cursorPosition + 1)) : value.LastIndexOf(' ', Mathf.Max(0, cursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(wordBound, cursorPosition, context);
    }
    private void MoveCursorChar(IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context, bool forward = false)
    {
        if(reason.Modifiers.HasFlag(EventModifiers.Shift))
        {
            TextCursorController.ChangeSelectionPosition(m_Value,
                                                         forward ? Mathf.Min(m_Value.Length, TextCursorController.SelectionPosition + 1)
                                                             : Mathf.Max(0, TextCursorController.SelectionPosition) - 1, context);
            return;
        }
        TextCursorController.ChangeCursorAndSelectionPositions(m_Value,
                                                               forward ? Mathf.Min(m_Value.Length, TextCursorController.CursorPosition) + 1
                                                                   : Mathf.Max(0, TextCursorController.CursorPosition - 1), context);
    }
    private void MoveCursorWord(IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context, bool forward = false)
    {
        int wordBound = forward ? m_Value.IndexOf(' ', TextCursorController.CursorPosition + 1)
                            : m_Value.LastIndexOf(' ', Mathf.Max(0, TextCursorController.CursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, m_Value.Length);
        MoveCursorTo(wordBound, reason, context);
    }
    private void MoveCursorToStart(IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context) => MoveCursorTo(0, reason, context);
    private void MoveCursorToEnd(IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context) => MoveCursorTo(m_Value.Length, reason, context);
    private void MoveCursorTo(int position, IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context)
    {
        if(reason.Modifiers.HasFlag(EventModifiers.Shift)) TextCursorController.ChangeCursorPosition(m_Value, position, context);
        TextCursorController.ChangeSelectionPosition(m_Value, position, context);
    }
    private void SelectAll(IOgGraphicsContext context)
    {
        TextCursorController.ChangeCursorPosition(m_Value, 0, context);
        TextCursorController.ChangeSelectionPosition(m_Value, m_Value.Length, context);
    }
    private void Cut(IOgGraphicsContext context)
    {
        if(TextCursorController.CursorPosition == TextCursorController.SelectionPosition) return;
        GUIUtility.systemCopyBuffer = GetSelectedText();
        DeleteSelectionIfNeeded(context);
    }
    private void Copy(IOgGraphicsContext context)
    {
        if(TextCursorController.CursorPosition == TextCursorController.SelectionPosition) return;
        GUIUtility.systemCopyBuffer = GetSelectedText();
    }
    private void Paste(IOgGraphicsContext context) => ReplaceSelection(GUIUtility.systemCopyBuffer, context);
    private string GetSelectedText()
    {
        int cursorPosition    = TextCursorController.CursorPosition;
        int selectionPosition = TextCursorController.SelectionPosition;
        if(cursorPosition == selectionPosition) return string.Empty;
        int startIndex = Mathf.Min(cursorPosition, selectionPosition);
        int length     = Mathf.Abs(cursorPosition - selectionPosition);
        return m_Value.Substring(startIndex, length);
    }
    private void DeleteRangeAndChangeCursorSelectionPositions(int from, int to, IOgGraphicsContext context)
    {
        if(to < from) (from, to) = (to, from);
        DeleteRange(from, to);
        TextCursorController.ChangeCursorAndSelectionPositions(m_Value, from, context);
    }
}