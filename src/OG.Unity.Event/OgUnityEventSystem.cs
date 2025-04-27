using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
using OG.Unity.Event.Prefab;
using System;
using UnityEngine;
using UeEvent = UnityEngine.Event;

namespace OG.Unity.Event;

public class OgUnityEventSystem(IOgGraphicsTool graphicsTool) : IOgEventSystem
{
    private OgUnityRepaintEvent? m_RepaintEvent;
    private OgUnityLayoutEvent? m_LayoutEvent;
    private OgUnityEvent? m_UsedEvent;

    private OgUnityMouseKeyDownEvent? m_MouseKeyDownEvent;
    private OgUnityMouseKeyUpEvent? m_MouseKeyUpEvent;
    private OgUnityMouseMoveEvent? m_MouseMoveEvent;
    private OgUnityMouseScrollEvent? m_MouseScrollEvent;

    private OgUnityKeyDownEvent? m_KeyDownEvent;
    private OgUnityKeyUpEvent? m_KeyUpEvent;

    public IOgEvent GetCurrent()
    {
        OgUnityEvent current = Parse(UeEvent.current);
        current.Reset();
        return current;
    }

    private OgUnityEvent Parse(UeEvent source) => Parse(source.type);

    private OgUnityEvent Parse(EventType type) => type switch
    {
        EventType.Repaint => m_RepaintEvent ??= new OgUnityRepaintEvent(graphicsTool),
        EventType.Layout => m_LayoutEvent ??= new OgUnityLayoutEvent(),
        EventType.Used => m_UsedEvent ??= new OgUnityEvent(),
        EventType.MouseDown => m_MouseKeyDownEvent ??= new OgUnityMouseKeyDownEvent(),
        EventType.MouseUp => m_MouseKeyUpEvent ??= new OgUnityMouseKeyUpEvent(),
        EventType.MouseDrag => m_MouseMoveEvent ??= new OgUnityMouseMoveEvent(),
        EventType.ScrollWheel => m_MouseScrollEvent ??= new OgUnityMouseScrollEvent(),
        EventType.KeyDown => m_KeyDownEvent ??= new OgUnityKeyDownEvent(),
        EventType.KeyUp => m_KeyUpEvent ??= new OgUnityKeyUpEvent(),
        _ => throw new NotImplementedException(),
    };
}
