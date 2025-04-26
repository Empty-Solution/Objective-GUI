using UnityEngine;

namespace OG.Common.Abstraction;

public readonly struct OgEvent(Event uEvent, Vector2 prevMousePosition, Matrix4x4 mainMatrix)
{
    public Event UEvent => uEvent;
    public EventType Type => uEvent.type;
    public Vector2 MousePosition => uEvent.mousePosition;
    public Vector2 LocalMousePosition => GUI.matrix.MultiplyPoint(mainMatrix.inverse.MultiplyPoint(new(MousePosition.x, MousePosition.y, 1)));
    public Vector2 MousePositionDelta => uEvent.mousePosition - prevMousePosition;
    public KeyCode KeyCode => uEvent.keyCode;
    public char Character => uEvent.character;

    public Vector2 ScrollDelta
    {
        get
        {
            Vector2 delta = uEvent.delta;
            return new(-delta.x, -delta.y);
        }
    }

    public int ClickCount => uEvent.clickCount;
    public bool IsMouseEvent => uEvent.isMouse;
    public bool IsKeyBoardEvent => uEvent.isKey;
    public bool ShiftModification => uEvent.shift;
    public bool AltModification => uEvent.alt;
    public bool ControlModification => uEvent.control;

    public void Use() => uEvent.Use();
}