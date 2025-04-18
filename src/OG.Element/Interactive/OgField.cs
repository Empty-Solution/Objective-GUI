using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using System;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgField<TElement, TScope>(string name, GUIStyle style, IOgTextStyle textStyle, TScope rootScope, IOgTransform transform, string value)
    : OgFocusableControl<TElement, TScope, string>(name, rootScope, transform, value) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void CursorPositionChangedHandler(OgField<TElement, TScope> instance, int cursorPosition, OgEvent reason);

    public delegate void SelectionPositionChangedHandler(OgField<TElement, TScope> instance, int cursorPosition, OgEvent reason);

    public bool Multiline { get; set; } = true;

    public GUIStyle Style { get; set; } = style;

    public IOgTextStyle TextStyle { get; set; } = textStyle;

    public Font Font => Style.font ?? GUI.skin.font ?? throw new NullReferenceException();

    public int CursorPosition { get; protected set; }

    public int SelectionPosition { get; protected set; }

    public event CursorPositionChangedHandler? OnCursorPositionChanged;
    public event SelectionPositionChangedHandler? OnSelectionPositionChanged;

    protected override void Focus(OgEvent reason)
    {
        base.Focus(reason);
        ChangeCursorAndSelectionPositions(reason);
    }

    protected override void HandleMouseDown(OgEvent reason)
    {
        base.HandleMouseDown(reason);
        ChangeCursorAndSelectionPositions(reason);
    }

    protected virtual void ChangeCursorAndSelectionPositions(OgEvent reason) => ChangeCursorAndSelectionPositions(reason, GetCharacterIndex(reason));

    protected virtual void ChangeCursorAndSelectionPositions(OgEvent reason, int position)
    {
        ChangeCursorPosition(position, reason);
        ChangeSelectionPosition(position, reason);
    }

    protected virtual int GetCharacterIndex(OgEvent reason) =>
        GetCharacterIndex(Value, reason.LocalMousePosition, Transform.LocalRect);

    protected virtual int GetCharacterIndex(string str, Vector2 mousePosition, Rect rect) =>
        Font.GetCharacterIndexByVector2(str, mousePosition, rect, TextStyle);

    protected virtual void ChangeCursorPosition(OgEvent reason) => ChangeCursorPosition(GetCharacterIndex(reason), reason);

    protected virtual void ChangeCursorPosition(int newPosition, OgEvent reason)
    {
        if(newPosition == CursorPosition) return;
        CursorPosition = newPosition;
        OnCursorPositionChanged?.Invoke(this, CursorPosition, reason);
    }

    protected virtual void ChangeSelectionPosition(OgEvent reason) => ChangeSelectionPosition(GetCharacterIndex(reason), reason);

    protected virtual void ChangeSelectionPosition(int newPosition, OgEvent reason)
    {
        if(newPosition == SelectionPosition) return;
        SelectionPosition = newPosition;
        OnSelectionPositionChanged?.Invoke(this, SelectionPosition, reason);
    }

    #region cursor interact

    protected override void BeginInteract(OgEvent reason)
    {
        base.BeginInteract(reason);
        ChangeCursorPosition(reason);
    }

    protected override void EndInteract(OgEvent reason)
    {
        base.EndInteract(reason);
        ChangeSelectionPosition(reason);
    }

    protected override void HandleMouseDrag(OgEvent reason)
    {
        base.HandleMouseDrag(reason);
        if(!IsFocusInteracting) return;
        ChangeSelectionPosition(reason);
        reason.Use();
    }

    #endregion

    #region text editing

    protected virtual void DeleteSelectionIfNeeded(OgEvent reason)
    {
        int selectionPosition = SelectionPosition;
        int cursorPosition = CursorPosition;

        if(cursorPosition == selectionPosition) return;

        DeleteSelection(cursorPosition, selectionPosition, reason);
    }

    protected virtual void DeleteSelection(int cursorPosition, int selectionPosition, OgEvent reason)
    {
        if(cursorPosition < selectionPosition) DeleteSelectionByCursor(cursorPosition, selectionPosition, reason);
        else DeleteSelectionBySelection(cursorPosition, selectionPosition, reason);
    }

    protected virtual void DeleteSelectionBySelection(int cursorPosition, int selectionPosition, OgEvent reason)
    {
        DeleteRange(selectionPosition, cursorPosition, reason);
        ChangeCursorPosition(selectionPosition, reason);
    }

    protected virtual void DeleteSelectionByCursor(int cursorPosition, int selectionPosition, OgEvent reason)
    {
        DeleteRange(cursorPosition, selectionPosition, reason);
        ChangeSelectionPosition(cursorPosition, reason);
    }

    protected virtual void DeleteRange(int from, int to, OgEvent reason) =>
        UpdateTextIfNeeded(Value.Remove(from, to - from), reason);

    protected virtual void DeleteRangeAndChangeCursorSelectionPositions(int from, int to, OgEvent reason)
    {
        if(to < from)
            (from, to) = (to, from);

        DeleteRange(from, to, reason);
        ChangeCursorAndSelectionPositions(reason, from);
    }

    protected virtual void ReplaceSelection(string replace, OgEvent reason)
    {
        int cursorPosition = CursorPosition;
        DeleteSelectionIfNeeded(reason);
        UpdateTextIfNeeded(Value.Insert(cursorPosition, replace), reason);
        ChangeCursorAndSelectionPositions(reason, cursorPosition + replace.Length);
    }

    protected virtual void Insert(char c, OgEvent reason) => ReplaceSelection(c.ToString(), reason);

    #endregion

    #region char handling

    private void UpdateTextIfNeeded(string text, OgEvent reason)
    {
        if(Value == text) return;
        ChangeValue(text, reason);
    }

    protected override void HandleKeyDown(OgEvent reason)
    {
        base.HandleKeyDown(reason);

        if(!IsFocused) return;

        if(HandleKeyEvent(reason))
        {
            reason.Use();
            return;
        }

        char chr = reason.Character;

        if(chr == '\n' && !Multiline) return;
        if(Font.HasCharacter(chr)) Insert(chr, reason);
    }

    #endregion

    #region key handling

    protected virtual bool HandleKeyEvent(OgEvent reason)
    {
        KeyCode keyCode = reason.KeyCode;

        switch(keyCode)
        {
            case KeyCode.Delete:
                if(reason.ControlModification)
                    DeleteWord(reason, true);
                else
                    DeleteChar(reason, true);
                break;
            case KeyCode.Backspace:
                if(reason.ControlModification)
                    DeleteWord(reason, false);
                else
                    DeleteChar(reason, false);
                break;
            case KeyCode.LeftArrow:
                if(reason.ControlModification)
                    MoveCursorWord(reason, false);
                else
                    MoveCursorChar(reason, false);
                break;
            case KeyCode.RightArrow:
                if(reason.ControlModification)
                    MoveCursorWord(reason, true);
                else
                    MoveCursorChar(reason, true);
                break;
            case KeyCode.Tab:
                HandleTab(reason);
                break;
            case KeyCode.Return:
            case KeyCode.KeypadEnter:
                HandleReturn(reason);
                break;
            case KeyCode.Home:
                MoveCursorToStart(reason);
                break;
            case KeyCode.End:
                MoveCursorToEnd(reason);
                break;
            case KeyCode.A when reason.ControlModification:
                SelectAll(reason);
                break;
            case KeyCode.X when reason.ControlModification:
                Cut(reason);
                break;
            case KeyCode.C when reason.ControlModification:
                Copy(reason);
                break;
            case KeyCode.V when reason.ControlModification:
                Paste(reason);
                break;
            default:
                return false;
        }

        return true;
    }

    protected virtual void HandleTab(OgEvent reason) =>
        Insert('\t', reason);

    protected virtual void HandleReturn(OgEvent reason)
    {
        if(!Multiline)
            return;

        Insert('\n', reason);
    }

    protected virtual void DeleteChar(OgEvent reason, bool forward)
    {
        int cursorPosition = CursorPosition;

        if(cursorPosition != SelectionPosition)
        {
            DeleteSelectionIfNeeded(reason);
            return;
        }

        int target = Mathf.Clamp(forward ? cursorPosition + 1 : cursorPosition - 1, 0, Value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(target, cursorPosition, reason);
    }

    protected virtual void DeleteWord(OgEvent reason, bool forward)
    {
        string value = Value;
        if(string.IsNullOrEmpty(value)) return;
        int cursorPosition = CursorPosition;
        int wordBound = forward ? value.IndexOf(' ', Mathf.Max(0, cursorPosition + 1)) : value.LastIndexOf(' ', Mathf.Max(0, cursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, value.Length);
        DeleteRangeAndChangeCursorSelectionPositions(wordBound, cursorPosition, reason);
    }

    protected virtual void MoveCursorChar(OgEvent reason, bool forward)
    {
        if(reason.ShiftModification)
        {
            ChangeSelectionPosition(forward ? Mathf.Min(Value.Length, SelectionPosition + 1) : Mathf.Max(0, SelectionPosition - 1), reason);
            return;
        }

        ChangeCursorAndSelectionPositions(reason, forward ? Mathf.Min(Value.Length, CursorPosition + 1) : Mathf.Max(0, CursorPosition - 1));
    }

    protected virtual void MoveCursorWord(OgEvent reason, bool forward)
    {
        int wordBound = forward ? Value.IndexOf(' ', CursorPosition + 1) : Value.LastIndexOf(' ', Mathf.Max(0, CursorPosition - 1));
        wordBound = Mathf.Clamp(wordBound, 0, Value.Length);
        MoveCursorTo(wordBound, reason);
    }

    protected virtual void MoveCursorToStart(OgEvent reason) => MoveCursorTo(0, reason);

    protected virtual void MoveCursorToEnd(OgEvent reason) => MoveCursorTo(Value.Length, reason);

    protected virtual void MoveCursorTo(int position, OgEvent reason)
    {
        if(reason.ShiftModification) ChangeCursorPosition(position, reason);
        ChangeSelectionPosition(position, reason);
    }

    protected virtual void SelectAll(OgEvent reason)
    {
        ChangeCursorPosition(0, reason);
        ChangeSelectionPosition(Value.Length, reason);
    }

    protected virtual void Cut(OgEvent reason)
    {
        if(CursorPosition == SelectionPosition) return;

        GUIUtility.systemCopyBuffer = GetSelectedText();
        DeleteSelectionIfNeeded(reason);
    }

    protected virtual void Copy(OgEvent reason)
    {
        if(CursorPosition == SelectionPosition) return;

        GUIUtility.systemCopyBuffer = GetSelectedText();
    }

    protected virtual void Paste(OgEvent reason) =>
        ReplaceSelection(GUIUtility.systemCopyBuffer, reason);

    protected virtual string GetSelectedText()
    {
        int cursorPosition = CursorPosition;
        int selectionPosition = SelectionPosition;

        if(cursorPosition == selectionPosition) return string.Empty;

        int startIndex = Mathf.Min(cursorPosition, selectionPosition);
        int length = Mathf.Abs(cursorPosition - selectionPosition);

        return Value.Substring(startIndex, length);
    }

    #endregion
}