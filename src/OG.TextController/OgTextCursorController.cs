using DK.Property.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
using System.IO;
using UnityEngine;
namespace OG.TextController;
public abstract class OgTextCursorController(IDkFieldProvider<Vector2>? localCursorPosition, IDkFieldProvider<Vector2>? localSelectionPosition)
    : IOgTextController
{
    private static GUIStyle tempStyle = new()
    {
        normal = new()
    };
    private static readonly GUIContent                 tempContent = new();
    public                  IDkFieldProvider<Vector2>? LocalCursorPosition    { get; } = localCursorPosition;
    public                  IDkFieldProvider<Vector2>? LocalSelectionPosition { get; } = localSelectionPosition;
    public                  int                        CursorPosition         { get; protected set; }
    public                  int                        SelectionPosition      { get; protected set; }
    public abstract bool HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgTextGraphicsContext context, out string newText);
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
        float xOffset      = position.x - context.RenderRect.x;
        float currentWidth = 0f;
        int   i            = 0;
        for(; i < text.Length; i++)
        {
            context.Font.GetCharacterInfo(text[i], out CharacterInfo info, context.FontSize, context.FontStyle);
            currentWidth += info.advance;
            if(currentWidth >= xOffset) return i;
        }
        return i;
    }
    private Vector2 GetCharPositionInString(string text, int characterIndex, IOgTextGraphicsContext context)
    {
        if(string.IsNullOrEmpty(text) || characterIndex < 0 || characterIndex >= text.Length || context.Font is null) return new();
        context.Font.RequestCharactersInTexture(text, context.FontSize, context.FontStyle);
        float xOffset     = 0f;
        for(int i = 0; i <= characterIndex; i++)
        {
            context.Font.GetCharacterInfo(text[i], out CharacterInfo info);
            xOffset += info.advance;
        }
        return new Vector2(xOffset + context.RenderRect.x, 0 + context.RenderRect.y) + CalculateOffset(context);
    }
    private static Vector2 CalculateOffset(IOgTextGraphicsContext context)
    {
        tempStyle.alignment = context.Alignment;
        tempStyle.fontStyle = context.FontStyle;
        tempStyle.fontSize  = context.FontSize;
        tempContent.text = context.Text;
        return GetAlignmentOffset(context.Alignment, context.RenderRect, tempStyle.CalcSize(tempContent));
    }
    private static Vector2 GetAlignmentOffset(TextAnchor alignment, Rect parentRect, Vector2 elementSize)
    {
        float offsetX = alignment switch
        {
            TextAnchor.UpperLeft or TextAnchor.MiddleLeft or TextAnchor.LowerLeft       => parentRect.x,
            TextAnchor.UpperCenter or TextAnchor.MiddleCenter or TextAnchor.LowerCenter => parentRect.x + ((parentRect.width - elementSize.x) * 0.5f),
            TextAnchor.UpperRight or TextAnchor.MiddleRight or TextAnchor.LowerRight    => parentRect.xMax - elementSize.x,
            _                                                                           => 0f
        };

        float offsetY = alignment switch
        {
            TextAnchor.UpperLeft or TextAnchor.UpperCenter or TextAnchor.UpperRight    => parentRect.y,
            TextAnchor.MiddleLeft or TextAnchor.MiddleCenter or TextAnchor.MiddleRight => parentRect.y + ((parentRect.height - elementSize.y) * 0.5f),
            TextAnchor.LowerLeft or TextAnchor.LowerCenter or TextAnchor.LowerRight    => parentRect.yMax - elementSize.y,
            _                                                                          => 0f
        };
        return new(offsetX, offsetY);
    }
}