using OG.Common.Abstraction;
using UnityEngine;

namespace OG.Element.Abstraction;

public interface IOgTextCursorController
{
    public delegate void CursorPositionChangedHandler(IOgTextCursorController instance, Vector2 cursorPosition, OgEvent reason);

    public delegate void SelectionPositionChangedHandler(IOgTextCursorController instance, Vector2 cursorPosition, OgEvent reason);

    int CursorPosition { get; }
    int SelectionPosition { get; }

    public event CursorPositionChangedHandler? OnCursorPositionChanged;
    public event SelectionPositionChangedHandler? OnSelectionPositionChanged;
    void ChangeSelectionPosition(OgEvent reason, string text, Rect rect);
    void ChangeCursorAndSelectionPositions(OgEvent reason, string text, Rect rect);
    void ChangeCursorPosition(OgEvent reason, string text, Rect rect);
    void ChangeSelectionPosition(OgEvent reason, int position, string text, Rect rect);
    void ChangeCursorAndSelectionPositions(OgEvent reason, int position, string text, Rect rect);
    void ChangeCursorPosition(OgEvent reason, int position, string text, Rect rect);
}