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
    string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context);
    string HandleCharacter(string text, char character, IOgTextGraphicsContext context);
    void ChangeCursorPosition(string text, Vector2 position, IOgTextGraphicsContext context);
    void ChangeSelectionPosition(string text, Vector2 position, IOgTextGraphicsContext context);
    void ChangeCursorAndSelectionPositions(string text, Vector2 position, IOgTextGraphicsContext context);
    void ChangeCursorPosition(string text, int position, IOgTextGraphicsContext context);
    void ChangeSelectionPosition(string text, int position, IOgTextGraphicsContext context);
    void ChangeCursorAndSelectionPositions(string text, int position, IOgTextGraphicsContext context);
}