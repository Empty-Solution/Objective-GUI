using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using OG.TextController.Abstraction;
namespace OG.TextController;
public abstract class OgCharacterTextController(IOgTextCursorController textCursorController, bool multiLine) : IOgTextController
{
    protected string                  m_Value = string.Empty;
    public    bool                    Multiline            { get; set; } = multiLine;
    public    IOgTextCursorController TextCursorController { get; }      = textCursorController;
    public abstract string HandleKeyEvent(string text, IOgKeyBoardKeyDownEvent reason, IOgGraphicsContext context);
    public string HandleCharacter(string text, char character, IOgGraphicsContext context)
    {
        if(character == '\n' && !Multiline) return text;
        m_Value = text;
        ReplaceSelection(character.ToString(), context);
        return m_Value;
    }
    protected virtual void DeleteSelectionIfNeeded(IOgGraphicsContext context)
    {
        int selectionPosition = TextCursorController.SelectionPosition;
        int cursorPosition    = TextCursorController.CursorPosition;
        if(cursorPosition == selectionPosition) return;
        DeleteSelection(cursorPosition, selectionPosition, context);
    }
    protected virtual void DeleteSelection(int cursorPosition, int selectionPosition, IOgGraphicsContext context)
    {
        if(cursorPosition < selectionPosition)
            DeleteSelectionByCursor(cursorPosition, selectionPosition, context);
        else
            DeleteSelectionBySelection(cursorPosition, selectionPosition, context);
    }
    protected virtual void DeleteSelectionBySelection(int cursorPosition, int selectionPosition, IOgGraphicsContext context)
    {
        DeleteRange(selectionPosition, cursorPosition);
        TextCursorController.ChangeCursorPosition(m_Value, selectionPosition, context);
    }
    protected virtual void DeleteSelectionByCursor(int cursorPosition, int selectionPosition, IOgGraphicsContext context)
    {
        DeleteRange(cursorPosition, selectionPosition);
        TextCursorController.ChangeSelectionPosition(m_Value, selectionPosition, context);
    }
    protected virtual void DeleteRange(int from, int to) => m_Value = m_Value.Remove(from, to - from);
    protected virtual void ReplaceSelection(string replace, IOgGraphicsContext context)
    {
        int cursorPosition = TextCursorController.CursorPosition;
        DeleteSelectionIfNeeded(context);
        m_Value = m_Value.Insert(cursorPosition, replace);
        TextCursorController.ChangeCursorAndSelectionPositions(m_Value, cursorPosition + replace.Length, context);
    }
}