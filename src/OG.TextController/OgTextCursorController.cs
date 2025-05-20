using DK.Property.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
using UnityEngine;
namespace OG.TextController;
public abstract class OgTextCursorController(IDkFieldProvider<Vector2>? localCursorPosition, IDkFieldProvider<Vector2>? localSelectionPosition)
    : IOgTextController
{
    private OgVertex[]                 m_Vertices = [];
    public  IDkFieldProvider<Vector2>? LocalCursorPosition    { get; } = localCursorPosition;
    public  IDkFieldProvider<Vector2>? LocalSelectionPosition { get; } = localSelectionPosition;
    public  int                        CursorPosition         { get; protected set; }
    public  int                        SelectionPosition      { get; protected set; }
    public abstract string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context);
    public abstract string HandleCharacter(string text, char character, IOgGraphicsContext context);
    public void ChangeCursorPosition(string text, Vector2 mousePosition, IOgGraphicsContext context) =>
        ChangeCursorPosition(text, GetCharacterIndex(text, mousePosition, context), context);
    public void ChangeSelectionPosition(string text, Vector2 mousePosition, IOgGraphicsContext context) =>
        ChangeSelectionPosition(text, GetCharacterIndex(text, mousePosition, context), context);
    public void ChangeCursorAndSelectionPositions(string text, Vector2 mousePosition, IOgGraphicsContext context)
    {
        ChangeSelectionPosition(text, mousePosition, context);
        ChangeCursorPosition(text, mousePosition, context);
    }
    public void ChangeCursorPosition(string text, int position, IOgGraphicsContext context)
    {
        CursorPosition = position;
        LocalCursorPosition!.Set(GetCharPositionInString(text, position, context));
    }
    public void ChangeSelectionPosition(string text, int position, IOgGraphicsContext context)
    {
        SelectionPosition = position;
        LocalSelectionPosition!.Set(GetCharPositionInString(text, position, context));
    }
    public void ChangeCursorAndSelectionPositions(string text, int position, IOgGraphicsContext context)
    {
        ChangeCursorPosition(text, position, context);
        ChangeSelectionPosition(text, position, context);
    }
    private int GetCharacterIndex(string text, Vector2 mousePosition, IOgGraphicsContext context) =>
        GetCharacterIndexByVector2(text, mousePosition, context);
    private int GetCharacterIndexByVector2(string text, Vector2 position, IOgGraphicsContext context)
    {
        if(string.IsNullOrEmpty(text)) return 0;
        int verticesCount = context.VerticesCount;
        if(verticesCount is 0) return 0;
        OgVertex[] vertices                          = m_Vertices;
        if(verticesCount > vertices.Length) vertices = m_Vertices = new OgVertex[verticesCount * 2];
        context.CopyVertices(vertices);
        
        float    lineHeight = m_Vertices[context.VerticesCount - 1].Position.y * context.Rect.height;
        string[] lines      = text.Split('\n');
        int      lineIndex  = (int)Mathf.Floor((context.Rect.y - position.y) / lineHeight);
        
        if(lineIndex < 0 || lineIndex >= lines.Length) return 0;
        string currentLineText = lines[lineIndex];
        float  xOffset         = position.x - context.Rect.x;
        float  currentWidth    = 0f;
        
        for(int i = 0; i < currentLineText.Length; i++)
        {
            currentWidth += m_Vertices[i == 0 ? 4 : i * 4].Position.x;
            if(!(currentWidth >= xOffset)) continue;
            int globalIndex                                = 0;
            for(int j = 0; j < lineIndex; j++) globalIndex += lines[j].Length + 1;
            return globalIndex + i;
        }
        return text.Length;
    }
    private Vector2 GetCharPositionInString(string text, int characterIndex, IOgGraphicsContext context)
    {
        if(string.IsNullOrEmpty(text) || characterIndex < 0 || characterIndex >= text.Length) return new();
        
        int verticesCount = context.VerticesCount;
        if(verticesCount is 0) return new();
        OgVertex[] vertices                          = m_Vertices;
        if(verticesCount > vertices.Length) vertices = m_Vertices = new OgVertex[verticesCount * 2];
        context.CopyVertices(vertices);
        
        float xOffset     = 0f;
        float yOffset     = 0f;
        int   currentLine = 0;
        
        float lineHeight  = m_Vertices[context.VerticesCount - 1].Position.y * context.Rect.height;
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
            xOffset += m_Vertices[i == 0 ? 4 : i * 4].Position.x;
        }
        return new((int)xOffset + context.Rect.x, (int)yOffset + context.Rect.y);
    }
}