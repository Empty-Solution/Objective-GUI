using DK.Property.Abstraction.Generic;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.TextController;
public abstract class OgCharacterTextController(IDkFieldProvider<Vector2>? localCursorPosition, IDkFieldProvider<Vector2>? localSelectionPosition)
    : OgTextCursorController(localCursorPosition, localSelectionPosition)
{
    protected string m_Value = string.Empty;
    public override string HandleCharacter(string text, char character, IOgTextGraphicsContext context)
    {
        if(character == '\n') return text;
        m_Value = text;
        ReplaceSelection(character.ToString(), context);
        return m_Value;
    }
    protected void DeleteSelectionIfNeeded(IOgTextGraphicsContext context)
    {
        int selectionPosition = SelectionPosition;
        int cursorPosition = CursorPosition;
        if(cursorPosition == selectionPosition) return;
        DeleteSelection(cursorPosition, selectionPosition, context);
    }
    protected void DeleteRange(int from, int to) => m_Value = m_Value.Remove(from, to - from);
    protected void ReplaceSelection(string replace, IOgTextGraphicsContext context)
    {
        int cursorPosition = CursorPosition;
        DeleteSelectionIfNeeded(context);
        m_Value = m_Value.Insert(cursorPosition, replace);
        ChangeCursorAndSelectionPositions(m_Value, cursorPosition + replace.Length, context);
    }
    private void DeleteSelection(int cursorPosition, int selectionPosition, IOgTextGraphicsContext context)
    {
        if(cursorPosition < selectionPosition)
            DeleteSelectionByCursor(cursorPosition, selectionPosition, context);
        else
            DeleteSelectionBySelection(cursorPosition, selectionPosition, context);
    }
    private void DeleteSelectionBySelection(int cursorPosition, int selectionPosition, IOgTextGraphicsContext context)
    {
        DeleteRange(selectionPosition, cursorPosition);
        ChangeCursorPosition(m_Value, selectionPosition, context);
    }
    private void DeleteSelectionByCursor(int cursorPosition, int selectionPosition, IOgTextGraphicsContext context)
    {
        DeleteRange(cursorPosition, selectionPosition);
        ChangeSelectionPosition(m_Value, selectionPosition, context);
    }
}