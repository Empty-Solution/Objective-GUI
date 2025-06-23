using DK.Property.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.TextController;
public class OgTextController(IDkFieldProvider<Vector2>? localCursorPosition, IDkFieldProvider<Vector2>? localSelectionPosition)
    : OgCharacterTextController(localCursorPosition, localSelectionPosition)
{
    public override bool HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context, out string newText)
    {
        m_Value = text;
        newText = m_Value;
        bool hasControl = reason.Modifiers.HasFlag(EventModifiers.Control);
        switch(reason.KeyCode)
        {
            case KeyCode.Delete:
            if(hasControl)
                DeleteWord(context, true);
            else
                DeleteChar(context, true);
            break;
            case KeyCode.Backspace:
            if(hasControl)
                DeleteWord(context);
            else
                DeleteChar(context);
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
            Copy();
            break;
            case KeyCode.V when hasControl:
            Paste(context);
            break;
            default: return false;
        }
        newText = m_Value;
        return true;
    }
    private void HandleTab(IOgTextGraphicsContext context) => m_Value = HandleCharacter(m_Value, '\t', context);
    private void DeleteChar(IOgTextGraphicsContext context, bool forward = false)
    {
        int cursorPosition = CursorPosition;
        if(cursorPosition != SelectionPosition)
        {
            DeleteSelectionIfNeeded(context);
            return;
        }
        int target = Mathf.Clamp(forward ? cursorPosition + 1 : cursorPosition - 1, 0, m_Value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(target, cursorPosition, context);
    }
    private void DeleteWord(IOgTextGraphicsContext context, bool forward = false)
    {
        string value = m_Value;
        if(string.IsNullOrEmpty(value)) return;
        int cursorPosition = CursorPosition;
        int wordBound = forward ? value.IndexOf(' ', Mathf.Max(0, cursorPosition + 1)) : value.LastIndexOf(' ', Mathf.Max(0, cursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(wordBound, cursorPosition, context);
    }
    private void MoveCursorChar(IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context, bool forward = false)
    {
        if(reason.Modifiers.HasFlag(EventModifiers.Shift))
        {
            ChangeSelectionPosition(m_Value, forward ? Mathf.Min(m_Value.Length, SelectionPosition + 1) : Mathf.Max(0, SelectionPosition) - 1, context);
            return;
        }
        ChangeCursorAndSelectionPositions(m_Value, forward ? Mathf.Min(m_Value.Length, CursorPosition) + 1 : Mathf.Max(0, CursorPosition - 1), context);
    }
    private void MoveCursorWord(IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context, bool forward = false)
    {
        int wordBound = forward ? m_Value.IndexOf(' ', CursorPosition + 1) : m_Value.LastIndexOf(' ', Mathf.Max(0, CursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, m_Value.Length);
        MoveCursorTo(wordBound, reason, context);
    }
    private void MoveCursorToStart(IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context) => MoveCursorTo(0, reason, context);
    private void MoveCursorToEnd(IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context) => MoveCursorTo(m_Value.Length, reason, context);
    private void MoveCursorTo(int position, IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context)
    {
        if(reason.Modifiers.HasFlag(EventModifiers.Shift)) ChangeCursorPosition(m_Value, position, context);
        ChangeSelectionPosition(m_Value, position, context);
    }
    private void SelectAll(IOgTextGraphicsContext context)
    {
        ChangeCursorPosition(m_Value, 0, context);
        ChangeSelectionPosition(m_Value, m_Value.Length, context);
    }
    private void Cut(IOgTextGraphicsContext context)
    {
        if(CursorPosition == SelectionPosition) return;
        GUIUtility.systemCopyBuffer = GetSelectedText();
        DeleteSelectionIfNeeded(context);
    }
    private void Copy()
    {
        if(CursorPosition == SelectionPosition) return;
        GUIUtility.systemCopyBuffer = GetSelectedText();
    }
    private void Paste(IOgTextGraphicsContext context) => ReplaceSelection(GUIUtility.systemCopyBuffer, context);
    private string GetSelectedText()
    {
        int cursorPosition = CursorPosition;
        int selectionPosition = SelectionPosition;
        if(cursorPosition == selectionPosition) return string.Empty;
        int startIndex = Mathf.Min(cursorPosition, selectionPosition);
        int length = Mathf.Abs(cursorPosition - selectionPosition);
        return m_Value.Substring(startIndex, length);
    }
    private void DeleteRangeAndChangeCursorSelectionPositions(int from, int to, IOgTextGraphicsContext context)
    {
        if(to < from) (from, to) = (to, from);
        DeleteRange(from, to);
        ChangeCursorAndSelectionPositions(m_Value, from, context);
    }
}