using OG.Common.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element;

public class OgTextCursorController(IOgTextStyle style) : IOgTextCursorController
{
    public event IOgTextCursorController.CursorPositionChangedHandler? OnCursorPositionChanged;
    public event IOgTextCursorController.SelectionPositionChangedHandler? OnSelectionPositionChanged;

    public int CursorPosition { get; private set; }
    public int SelectionPosition { get; private set; }

    public void ChangeCursorAndSelectionPositions(OgEvent reason, string text, Rect rect)
    {
        ChangeCursorPosition(reason, text, rect);
        ChangeSelectionPosition(reason, text, rect);
    }

    public void ChangeCursorPosition(OgEvent reason, string text, Rect rect) =>
        ChangeCursorPosition(reason, GetCharacterIndex(reason, text, rect), text, rect);

    public void ChangeSelectionPosition(OgEvent reason, string text, Rect rect) =>
        ChangeSelectionPosition(reason, GetCharacterIndex(reason, text, rect), text, rect);

    public void ChangeCursorAndSelectionPositions(OgEvent reason, int position, string text, Rect rect)
    {
        ChangeCursorPosition(reason, position, text, rect);
        ChangeSelectionPosition(reason, position, text, rect);
    }

    public void ChangeCursorPosition(OgEvent reason, int position, string text, Rect rect)
    {
        if(position == CursorPosition) return;
        CursorPosition = position;
        OnCursorPositionChanged?.Invoke(this, style.Font.GetCharPositionInString(text, CursorPosition, style, rect), reason);
    }

    public void ChangeSelectionPosition(OgEvent reason, int position, string text, Rect rect)
    {
        if(position == SelectionPosition) return;
        SelectionPosition = position;
        OnSelectionPositionChanged?.Invoke(this, style.Font.GetCharPositionInString(text, CursorPosition, style, rect), reason);
    }

    private int GetCharacterIndex(OgEvent reason, string text, Rect rect) =>
        GetCharacterIndex(text, reason.LocalMousePosition, rect);

    private int GetCharacterIndex(string text, Vector2 mousePosition, Rect rect) =>
        style.Font.GetCharacterIndexByVector2(text, mousePosition, rect, style);
}