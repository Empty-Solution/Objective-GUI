using DK.Property.Abstraction.Generic;
using OG.DataTypes.FontStyle;
using OG.DataTypes.Rectangle;
using OG.DataTypes.TextAnchor;
using OG.DataTypes.Vector;
using OG.Event.Abstraction;
using OG.TextCursorController.Abstraction;
using System;

namespace OG.TextCursorController;

public abstract class OgTextCursorController(IDkProperty<int> cursorPosition, IDkProperty<int> selectionPosition) : IOgTextCursorController
{
    public IDkProperty<int>?           FontSize               { get; set; }
    public IDkProperty<EOgTextAnchor>? Alignment              { get; set; }
    public IDkProperty<EOgFontStyle>?  FontStyle              { get; set; }
    public IDkProperty<OgVector2>?     LocalCursorPosition    { get; set; }
    public IDkProperty<OgVector2>?     LocalSelectionPosition { get; set; }
    public IDkProperty<int>            CursorPosition         { get; set; } = cursorPosition;
    public IDkProperty<int>            SelectionPosition      { get; set; } = selectionPosition;

    public void ChangeSelectionPosition(string text, OgRectangle rect, IOgMouseEvent reason)
    {
        int position = GetCharacterIndex(reason, text, rect);
        SelectionPosition.Set(position);
        LocalCursorPosition!.Set(GetCharPositionInString(text, position, rect));
    }

    public void ChangeCursorPosition(string text, OgRectangle rect, IOgMouseEvent reason)
    {
        int position = GetCharacterIndex(reason, text, rect);
        CursorPosition.Set(position);
        LocalSelectionPosition!.Set(GetCharPositionInString(text, position, rect));
    }

    public void ChangeCursorAndSelectionPositions(int position)
    {
        _ = CursorPosition.Set(position);
        _ = SelectionPosition.Set(position);
    }

    public void ChangeCursorAndSelectionPositions(string text, OgRectangle rect, IOgMouseEvent reason)
    {
        ChangeSelectionPosition(text, rect, reason);
        ChangeCursorPosition(text, rect, reason);
    }

    private int GetCharacterIndex(IOgMouseEvent reason, string text, OgRectangle rect) => GetCharacterIndex(text, reason.LocalMousePosition, rect);

    private int GetCharacterIndex(string text, OgVector2 mousePosition, OgRectangle rect) => GetCharacterIndexByVector2(text, mousePosition, rect);

    protected abstract OgVector2 CalSize(string text, OgRectangle textRect, float realLineHeight);
    protected abstract void RequestCharacters(string text, int fontSize, EOgFontStyle fontStyle);
    protected abstract float GetCharacterAdvance(char character, int fontSize, EOgFontStyle fontStyle);
    protected abstract float GetLineHeight(int fontSize);

    private OgVector2 GetCharPositionInString(string text, int characterIndex, OgRectangle textRect)
    {
        if(string.IsNullOrEmpty(text) || characterIndex < 0 || characterIndex >= text.Length) return new();

        float xOffset = 0f;
        float yOffset = 0f;
        RequestCharacters(text, FontSize?.Get() ?? 14, FontStyle?.Get() ?? EOgFontStyle.NORMAL);

        float realLineHeight = GetLineHeight(FontSize?.Get() ?? 14);

        int currentLine = 0;

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

            xOffset += GetCharacterAdvance(currentChar, FontSize?.Get() ?? 14, FontStyle?.Get() ?? EOgFontStyle.NORMAL);
        }

        return new OgVector2((int)xOffset, (int)yOffset) + GetAlignmentOffset(text, textRect, realLineHeight);
    }

    private int GetCharacterIndexByVector2(string text, OgVector2 position, OgRectangle textRect)
    {
        if(string.IsNullOrEmpty(text)) return 0;

        string[] lines = text.Split('\n');

        RequestCharacters(text, FontSize?.Get() ?? 14, FontStyle?.Get() ?? EOgFontStyle.NORMAL);
        int lineIndex = (int)Math.Floor((textRect.Y - position.Y) / GetLineHeight(FontSize?.Get() ?? 14));

        if(lineIndex < 0 || lineIndex >= lines.Length) return 0;

        string currentLineText = lines[lineIndex];

        float xOffset = position.X - textRect.X;

        float currentWidth = 0f;

        for(int i = 0; i < currentLineText.Length; i++)
        {
            currentWidth += GetCharacterAdvance(currentLineText[i], FontSize?.Get() ?? 14, FontStyle?.Get() ?? EOgFontStyle.NORMAL);

            if(!(currentWidth >= xOffset)) continue;

            int globalIndex = 0;

            for(int j = 0; j < lineIndex; j++) globalIndex += lines[j].Length + 1;

            return globalIndex + i;
        }

        return text.Length;
    }

    private OgVector2 GetAlignmentOffset(string text, OgRectangle textRect, float realLineHeight) =>
        GetAlignmentOffset(textRect, CalSize(text, textRect, realLineHeight));

    private OgVector2 GetAlignmentOffset(OgRectangle parentRect, OgVector2 elementSize)
    {
        if(Alignment is null) return new(parentRect.X, parentRect.Y);

        float offsetX = Alignment.Get() switch
        {
            EOgTextAnchor.UPPER_LEFT or EOgTextAnchor.UPPER_CENTER or EOgTextAnchor.UPPER_RIGHT    => parentRect.X,
            EOgTextAnchor.MIDDLE_LEFT or EOgTextAnchor.MIDDLE_CENTER or EOgTextAnchor.MIDDLE_RIGHT => parentRect.X    + ((parentRect.Width - elementSize.X) * 0.5f),
            EOgTextAnchor.LOWER_LEFT or EOgTextAnchor.LOWER_CENTER or EOgTextAnchor.LOWER_RIGHT    => parentRect.XMax - elementSize.X,
            _                                                                                      => 0f
        };

        float offsetY = Alignment.Get() switch
        {
            EOgTextAnchor.UPPER_LEFT or EOgTextAnchor.UPPER_CENTER or EOgTextAnchor.UPPER_RIGHT    => parentRect.Y,
            EOgTextAnchor.MIDDLE_LEFT or EOgTextAnchor.MIDDLE_CENTER or EOgTextAnchor.MIDDLE_RIGHT => parentRect.Y    + ((parentRect.Height - elementSize.Y) * 0.5f),
            EOgTextAnchor.LOWER_LEFT or EOgTextAnchor.LOWER_CENTER or EOgTextAnchor.LOWER_RIGHT    => parentRect.YMax - elementSize.Y,
            _                                                                                      => 0f
        };
        return new((int)offsetX, (int)offsetY);
    }
}