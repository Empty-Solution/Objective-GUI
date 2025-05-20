using DK.Property.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.TextController.Abstraction;
public interface IOgTextController
{
    IDkFieldProvider<Vector2>? LocalCursorPosition    { get; }
    IDkFieldProvider<Vector2>? LocalSelectionPosition { get; }
    int                        CursorPosition         { get; }
    int                        SelectionPosition      { get; }
    string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context);
    string HandleCharacter(string text, char character, IOgGraphicsContext context);
    void ChangeCursorPosition(string text, Vector2 position, IOgGraphicsContext context);
    void ChangeSelectionPosition(string text, Vector2 position, IOgGraphicsContext context);
    void ChangeCursorAndSelectionPositions(string text, Vector2 position, IOgGraphicsContext context);
    void ChangeCursorPosition(string text, int position, IOgGraphicsContext context);
    void ChangeSelectionPosition(string text, int position, IOgGraphicsContext context);
    void ChangeCursorAndSelectionPositions(string text, int position, IOgGraphicsContext context);
}