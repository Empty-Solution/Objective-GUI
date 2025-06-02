using DK.Property.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
using UnityEngine;
namespace OG.TextController;
public abstract class OgTextCursorController(IDkFieldProvider<Vector2>? localCursorPosition, IDkFieldProvider<Vector2>? localSelectionPosition)
    : IOgTextController
{
    public IDkFieldProvider<Vector2>? LocalCursorPosition    { get; } = localCursorPosition;
    public IDkFieldProvider<Vector2>? LocalSelectionPosition { get; } = localSelectionPosition;
    public int                        CursorPosition         { get; protected set; }
    public int                        SelectionPosition      { get; protected set; }
    public abstract string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context);
    public abstract string HandleCharacter(string text, char character, IOgTextGraphicsContext context);
    public void ChangeCursorPosition(string text, Vector2 mousePosition, IOgTextGraphicsContext context) =>
        ChangeCursorPosition(text, GetCharacterIndex(text, mousePosition, context), context);
    public void ChangeSelectionPosition(string text, Vector2 mousePosition, IOgTextGraphicsContext context) =>
        ChangeSelectionPosition(text, GetCharacterIndex(text, mousePosition, context), context);
    public void ChangeCursorAndSelectionPositions(string text, Vector2 mousePosition, IOgTextGraphicsContext context)
    {
        ChangeSelectionPosition(text, mousePosition, context);
        ChangeCursorPosition(text, mousePosition, context);
    }
    public void ChangeCursorPosition(string text, int position, IOgTextGraphicsContext context)
    {
        CursorPosition = position;
        LocalCursorPosition!.Set(GetCharPositionInString(text, position, context));
    }
    public void ChangeSelectionPosition(string text, int position, IOgTextGraphicsContext context)
    {
        SelectionPosition = position;
        LocalSelectionPosition!.Set(GetCharPositionInString(text, position, context));
    }
    public void ChangeCursorAndSelectionPositions(string text, int position, IOgTextGraphicsContext context)
    {
        ChangeCursorPosition(text, position, context);
        ChangeSelectionPosition(text, position, context);
    }
    private int GetCharacterIndex(string text, Vector2 mousePosition, IOgTextGraphicsContext context) =>
        GetCharacterIndexByVector2(text, mousePosition, context);
    private int GetCharacterIndexByVector2(string text, Vector2 position, IOgTextGraphicsContext context)
    {
        if(string.IsNullOrEmpty(text) || context.Font is null) return 0;
        context.Font.RequestCharactersInTexture(text, context.FontSize, context.FontStyle);
        float    lineHeight = context.Font.lineHeight;
        string[] lines      = text.Split('\n');
        int      lineIndex  = (int)Mathf.Floor((context.RenderRect.y - position.y) / lineHeight);
        if(lineIndex < 0 || lineIndex >= lines.Length) return 0;
        string currentLineText = lines[lineIndex];
        float  xOffset         = position.x - context.RenderRect.x;
        float  currentWidth    = 0f;
        for(int i = 0; i < currentLineText.Length; i++)
        {
            context.Font.GetCharacterInfo(text[i], out CharacterInfo info);
            currentWidth += info.advance;
            if(!(currentWidth >= xOffset)) continue;
            int globalIndex                                = 0;
            for(int j = 0; j < lineIndex; j++) globalIndex += lines[j].Length + 1;
            return globalIndex + i;
        }
        return text.Length;
    }
    private Vector2 GetCharPositionInString(string text, int characterIndex, IOgTextGraphicsContext context)
    {
        if(string.IsNullOrEmpty(text) || characterIndex < 0 || characterIndex >= text.Length || context.Font is null) return new();
        context.Font.RequestCharactersInTexture(text, context.FontSize, context.FontStyle);
        float xOffset     = 0f;
        float yOffset     = 0f;
        int   currentLine = 0;
        float lineHeight  = context.Font.lineHeight;
        for(int i = 0; i <= characterIndex; i++)
        {
            char currentChar = text[i];
            if(currentChar == '\n')
            {
                currentLine++;
                yOffset = -currentLine * lineHeight;
                xOffset = 0f;
                continue;
            }
            context.Font.GetCharacterInfo(text[i], out CharacterInfo info);
            xOffset += info.advance;
        }
        return new((int)xOffset + context.RenderRect.x, (int)yOffset + context.RenderRect.y);
    }
}