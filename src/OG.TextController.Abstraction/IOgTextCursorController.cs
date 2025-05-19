using DK.Property.Abstraction.Generic;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.TextController.Abstraction;
public interface IOgTextCursorController
{
    IDkFieldProvider<Vector2>? LocalCursorPosition    { get; }
    IDkFieldProvider<Vector2>? LocalSelectionPosition { get; }
    int                        CursorPosition         { get; }
    int                        SelectionPosition      { get; }
    void ChangeCursorPosition(string text, Vector2 position, IOgGraphicsContext context);
    void ChangeSelectionPosition(string text, Vector2 position, IOgGraphicsContext context);
    void ChangeCursorAndSelectionPositions(string text, Vector2 position, IOgGraphicsContext context);
    void ChangeCursorPosition(string text, int position, IOgGraphicsContext context);
    void ChangeSelectionPosition(string text, int position, IOgGraphicsContext context);
    void ChangeCursorAndSelectionPositions(string text, int position, IOgGraphicsContext context);
}