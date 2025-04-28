using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
using OG.Unity.Event.Prefab;
using System;
using UnityEngine;
using UeEvent = UnityEngine.Event;

namespace OG.Unity.Event;

public class OgUnityEventSystem(IOgGraphicsTool graphicsTool) : IOgEventSystem
{
    private OgUnityKeyDownEvent? m_KeyDownEvent;
    private OgUnityKeyUpEvent? m_KeyUpEvent;
    private OgUnityLayoutEvent? m_LayoutEvent;

    private OgUnityMouseKeyDownEvent? m_MouseKeyDownEvent;
    private OgUnityMouseKeyUpEvent? m_MouseKeyUpEvent;
    private OgUnityMouseMoveEvent? m_MouseMoveEvent;
    private OgUnityMouseScrollEvent? m_MouseScrollEvent;
    private OgUnityRepaintEvent? m_RepaintEvent;
    private OgUnityEvent? m_UsedEvent;

    public IOgEvent GetCurrent()
    {
        OgUnityEvent current = Parse(UeEvent.current);
        current.Reset();
        return current;
    }

    private OgUnityEvent Parse(UeEvent source) => Parse(source.type);

    private OgUnityEvent Parse(EventType type) => type switch
    {
        EventType.Repaint => m_RepaintEvent ??= new(graphicsTool),
        EventType.Layout => m_LayoutEvent ??= new(),
        EventType.Used => m_UsedEvent ??= new(),
        EventType.MouseDown => m_MouseKeyDownEvent ??= new(),
        EventType.MouseUp => m_MouseKeyUpEvent ??= new(),
        EventType.MouseDrag => m_MouseMoveEvent ??= new(),
        EventType.ScrollWheel => m_MouseScrollEvent ??= new(),
        EventType.KeyDown => m_KeyDownEvent ??= new(),
        EventType.KeyUp => m_KeyUpEvent ??= new(),
        _ => throw new NotImplementedException()
    };
}