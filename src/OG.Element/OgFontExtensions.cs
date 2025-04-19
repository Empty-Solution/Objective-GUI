using OG.Element.Legacy.Tools;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element;

public static class OgFontExtensions
{
    public static Vector2 GetCharPositionInString(this Font font, string text, int characterIndex, IOgTextStyle textStyle, Rect textRect)
    {
        if(font == null || string.IsNullOrEmpty(text) || characterIndex < 0 || characterIndex >= text.Length)
            return Vector2.zero;

        font.RequestCharactersInTexture(text, textStyle.FontSize, textStyle.FontStyle);

        float xOffset = 0f;
        float yOffset = 0f;

        float realLineHeight = font.lineHeight * (textStyle.FontSize / (float)font.fontSize);

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

            if(font.GetCharacterInfo(currentChar, out CharacterInfo characterInfo, textStyle.FontSize, textStyle.FontStyle))
                xOffset += characterInfo.advance;
        }

        return new Vector2(xOffset, yOffset) + GetAlignmentOffset(text, textRect, textStyle);
    }

    public static int GetCharacterIndexByVector2(this Font font, string text, Vector2 position, Rect textRect, IOgTextStyle textStyle)
    {
        if(string.IsNullOrEmpty(text))
            return 0;

        string[] lines = text.Split('\n');
        int totalLines = lines.Length;

        int lineIndex = Mathf.FloorToInt((textRect.y - position.y) / font.lineHeight * (textStyle.FontSize / (float)font.fontSize));
        if(lineIndex < 0 || lineIndex >= totalLines)
            return 0;

        string currentLineText = lines[lineIndex];

        float xOffset = position.x - textRect.x;

        font.RequestCharactersInTexture(currentLineText, textStyle.FontSize, textStyle.FontStyle);
        float currentWidth = 0f;

        for(int i = 0; i < currentLineText.Length; i++)
        {
            char currentChar = currentLineText[i];

            if(!font.GetCharacterInfo(currentChar, out CharacterInfo characterInfo, textStyle.FontSize, textStyle.FontStyle))
                continue;

            currentWidth += characterInfo.advance;

            if(!(currentWidth >= xOffset))
                continue;

            int globalIndex = 0;

            for(int j = 0; j < lineIndex; j++)
                globalIndex += lines[j].Length + 1;

            return globalIndex + i;
        }

        return text.Length;
    }

    private static Vector2 GetAlignmentOffset(string text, Rect textRect, IOgTextStyle textStyle)
    {
        GUIStyle unityStyle = InternalLegacyGuiStyleTool.GetUnityStyle(textStyle);
        GUIContent tempContent = InternalLegacyGuiContentTool.GetContent(text);
        return textStyle.Alignment.GetAlignmentOffset(textRect, unityStyle.CalcSize(tempContent));
    }
}