using OG.DataTypes.KeyboardModifier;
using OG.DataTypes.KeyCode;
using OG.DataTypes.Vector;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
using OG.Unity.Event.Prefab;
using System;
using UnityEngine;
using UeEvent = UnityEngine.Event;
namespace OG.Unity.Event;
public class OgUnityEventSystem(IOgGraphicsTool graphicsTool) : IOgEventSystem
{
    private OgUnityKeyDownEvent?      m_KeyDownEvent;
    private OgUnityKeyUpEvent?        m_KeyUpEvent;
    private OgVector2                 m_LastMousePosition;
    private OgUnityLayoutEvent?       m_LayoutEvent;
    private OgUnityMouseKeyDownEvent? m_MouseKeyDownEvent;
    private OgUnityMouseKeyUpEvent?   m_MouseKeyUpEvent;
    private OgUnityMouseMoveEvent?    m_MouseMoveEvent;
    private OgUnityMouseScrollEvent?  m_MouseScrollEvent;
    private OgUnityRepaintEvent?      m_RepaintEvent;
    private OgUnityEvent?             m_UsedEvent;
    public IOgEvent GetCurrent()
    {
        OgUnityEvent current = Parse(UeEvent.current);
        current.Reset();
        return current;
    }
    private OgUnityEvent Parse(UeEvent source) => source.type switch
    {
        EventType.Repaint     => m_RepaintEvent ??= new(graphicsTool),
        EventType.Layout      => m_LayoutEvent ??= new(),
        EventType.Used        => m_UsedEvent ??= new(),
        EventType.MouseDown   => ParseMouseDownEvent(source),
        EventType.MouseUp     => ParseMouseUpEvent(source),
        EventType.MouseDrag   => ParseMouseMoveEvent(source),
        EventType.ScrollWheel => ParseMouseScrollEvent(source),
        EventType.KeyDown     => ParseKeyDownEvent(source),
        EventType.KeyUp       => ParseKeyUpEvent(source),
        _                     => throw new NotImplementedException()
    };
    private OgUnityMouseMoveEvent ParseMouseMoveEvent(UeEvent source)
    {
        OgUnityMouseMoveEvent target = m_MouseMoveEvent ??= new();
        FillMouseEvent(source, target);
        OgVector2 mousePosition = ParseVector(source.mousePosition);
        target.MouseMoveDelta = m_LastMousePosition - mousePosition;
        m_LastMousePosition   = mousePosition;
        return target;
    }
    private OgUnityMouseScrollEvent ParseMouseScrollEvent(UeEvent source)
    {
        OgUnityMouseScrollEvent target = m_MouseScrollEvent ??= new();
        FillMouseEvent(source, target);
        target.ScrollDelta = ParseVector(source.delta);
        return target;
    }
    private OgUnityMouseKeyDownEvent ParseMouseDownEvent(UeEvent source)
    {
        OgUnityMouseKeyDownEvent target = m_MouseKeyDownEvent ??= new();
        FillMouseEvent(source, target);
        return target;
    }
    private OgUnityMouseKeyUpEvent ParseMouseUpEvent(UeEvent source)
    {
        OgUnityMouseKeyUpEvent target = m_MouseKeyUpEvent ??= new();
        FillMouseEvent(source, target);
        return target;
    }
    private OgUnityKeyDownEvent ParseKeyDownEvent(UeEvent source)
    {
        OgUnityKeyDownEvent target = m_KeyDownEvent ??= new();
        FillKeyboardEvent(source, target);
        FillKeyEvent(source, target);
        return target;
    }
    private OgUnityKeyUpEvent ParseKeyUpEvent(UeEvent source)
    {
        OgUnityKeyUpEvent target = m_KeyUpEvent ??= new();
        FillKeyboardEvent(source, target);
        FillKeyEvent(source, target);
        return target;
    }
    private static void FillKeyEvent(UeEvent source, OgUnityKeyEvent target)
    {
        target.KeyCode   = (EOgKeyCode)source.keyCode;
        target.Character = source.character;
    }
    private static void FillKeyboardEvent(UeEvent source, OgUnityKeyboardEvent target)
    {
        target.Modifier =  EOgKeyboardModifier.NONE;
        target.Modifier |= source.control ? EOgKeyboardModifier.CONTROL : EOgKeyboardModifier.NONE;
        target.Modifier |= source.shift ? EOgKeyboardModifier.SHIFT : EOgKeyboardModifier.NONE;
    }
    private static void      FillMouseEvent(UeEvent source, OgUnityMouseEvent target) => target.LocalMousePosition = ParseVector(source.mousePosition);
    private static OgVector2 ParseVector(Vector2    source) => new((int)source.x, (int)source.y);
}