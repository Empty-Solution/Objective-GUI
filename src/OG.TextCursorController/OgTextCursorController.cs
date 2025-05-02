using DK.Property.Abstraction.Generic;
using OG.DataTypes.Vector;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction.Contexts;
using OG.TextCursorController.Abstraction;
using System;
using System.Linq;
namespace OG.TextCursorController;
public class OgTextCursorController(IDkProperty<int> cursorPosition, IDkProperty<int> selectionPosition) : IOgTextCursorController
{
    public IDkProperty<OgVector2>? LocalCursorPosition    { get; set; }
    public IDkProperty<OgVector2>? LocalSelectionPosition { get; set; }
    public IDkProperty<int>        CursorPosition         { get; set; } = cursorPosition;
    public IDkProperty<int>        SelectionPosition      { get; set; } = selectionPosition;
    public void ChangeSelectionPosition(string text, IOgMouseEvent reason, OgTextRepaintContext context)
    {
        int position = GetCharacterIndex(text, reason.LocalMousePosition, context);
        SelectionPosition.Set(position);
        LocalCursorPosition!.Set(GetCharPositionInString(text, position, context));
    }
    public void ChangeCursorPosition(string text, IOgMouseEvent reason, OgTextRepaintContext context)
    {
        int position = GetCharacterIndex(text, reason.LocalMousePosition, context);
        CursorPosition.Set(position);
        LocalSelectionPosition!.Set(GetCharPositionInString(text, position, context));
    }
    public void ChangeCursorAndSelectionPositions(int position)
    {
        _ = CursorPosition.Set(position);
        _ = SelectionPosition.Set(position);
    }
    public void ChangeCursorAndSelectionPositions(string text, IOgMouseEvent reason, OgTextRepaintContext context)
    {
        ChangeSelectionPosition(text, reason, context);
        ChangeCursorPosition(text, reason, context);
    }
    private int GetCharacterIndex(string text, OgVector2 mousePosition, OgTextRepaintContext context) =>
        GetCharacterIndexByVector2(text, mousePosition, context);
    private int GetCharacterIndexByVector2(string text, OgVector2 position, OgTextRepaintContext context)
    {
        if(string.IsNullOrEmpty(text)) return 0;
        string[] lines     = text.Split('\n');
        int      lineIndex = (int)Math.Floor((context.RepaintRect.Y - position.Y) / context.LineHeight);
        if(lineIndex < 0 || lineIndex >= lines.Length) return 0;
        string currentLineText = lines[lineIndex];
        float  xOffset         = position.X - context.RepaintRect.X;
        float  currentWidth    = 0f;
        for(int i = 0; i < currentLineText.Length; i++)
        {
            currentWidth += context.CharsSizes.ElementAt(i);
            if(!(currentWidth >= xOffset)) continue;
            int globalIndex                                = 0;
            for(int j = 0; j < lineIndex; j++) globalIndex += lines[j].Length + 1;
            return globalIndex + i;
        }
        return text.Length;
    }
    private OgVector2 GetCharPositionInString(string text, int characterIndex, OgTextRepaintContext context)
    {
        if(string.IsNullOrEmpty(text) || characterIndex < 0 || characterIndex >= text.Length) return new();
        float xOffset        = 0f;
        float yOffset        = 0f;
        float realLineHeight = context.LineHeight;
        int   currentLine    = 0;
        for(int i = 0; i <= characterIndex; i++)
        {
            char currentChar = text[i];
            if(currentChar == '\n')
            {
                currentLine++;
                yOffset = -currentLine * realLineHeight;
                xOffset = 0f;
                continue;
            }
            xOffset += context.CharsSizes.ElementAt(i);
        }
        return new((int)xOffset + context.RepaintRect.X, (int)yOffset + context.RepaintRect.Y);
    }
}