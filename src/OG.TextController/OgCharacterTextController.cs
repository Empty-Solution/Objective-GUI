using DK.Property.Abstraction.Generic;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.TextController;
public abstract class OgCharacterTextController(bool multiLine, IDkFieldProvider<Vector2>? localCursorPosition,
    IDkFieldProvider<Vector2>? localSelectionPosition) : OgTextCursorController(localCursorPosition, localSelectionPosition)
{
    protected string m_Value = string.Empty;
    public    bool   Multiline { get; set; } = multiLine;
    public override string HandleCharacter(string text, char character, IOgGraphicsContext context)
    {
        if(character == '\n' && !Multiline) return text;
        m_Value = text;
        ReplaceSelection(character.ToString(), context);
        return m_Value;
    }
    protected void DeleteSelectionIfNeeded(IOgGraphicsContext context)
    {
        int selectionPosition = SelectionPosition;
        int cursorPosition    = CursorPosition;
        if(cursorPosition == selectionPosition) return;
        DeleteSelection(cursorPosition, selectionPosition, context);
    }
    protected void DeleteRange(int from, int to) => m_Value = m_Value.Remove(from, to - from);
    protected void ReplaceSelection(string replace, IOgGraphicsContext context)
    {
        int cursorPosition = CursorPosition;
        DeleteSelectionIfNeeded(context);
        m_Value = m_Value.Insert(cursorPosition, replace);
        ChangeCursorAndSelectionPositions(m_Value, cursorPosition + replace.Length, context);
    }
    private void DeleteSelection(int cursorPosition, int selectionPosition, IOgGraphicsContext context)
    {
        if(cursorPosition < selectionPosition)
            DeleteSelectionByCursor(cursorPosition, selectionPosition, context);
        else
            DeleteSelectionBySelection(cursorPosition, selectionPosition, context);
    }
    private void DeleteSelectionBySelection(int cursorPosition, int selectionPosition, IOgGraphicsContext context)
    {
        DeleteRange(selectionPosition, cursorPosition);
        ChangeCursorPosition(m_Value, selectionPosition, context);
    }
    private void DeleteSelectionByCursor(int cursorPosition, int selectionPosition, IOgGraphicsContext context)
    {
        DeleteRange(cursorPosition, selectionPosition);
        ChangeSelectionPosition(m_Value, selectionPosition, context);
    }
}