using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
// using OG.Unity.Event.Prefab;
namespace OG.Unity.Event;
public class OgUnityEventSystem(IOgGraphicsTool graphicsTool) // : IOgEventSystem
{
    /*
    private OgUnityKeyDownEvent?      m_KeyDownEvent;
    private OgUnityKeyUpEvent?        m_KeyUpEvent;
    private OgUnityLayoutEvent?       m_LayoutEvent;
    private OgUnityMouseKeyDownEvent? m_MouseKeyDownEvent;
    private OgUnityMouseKeyUpEvent?   m_MouseKeyUpEvent;
    private OgUnityMouseMoveEvent?    m_MouseMoveEvent;
    private OgUnityMouseScrollEvent?  m_MouseScrollEvent;
    private OgUnityRepaintEvent?      m_RepaintEvent;
    public IOgEvent GetCurrent()
    {
        UeEvent      source  = UeEvent.current;
        OgUnityEvent current = Parse(source);
        current.Reset();
        current.Fill(source);
        return current;
    }
    private OgUnityEvent Parse(UeEvent source) => source.type switch
    {
        EventType.Repaint     => m_RepaintEvent ??= new(graphicsTool),
        EventType.Layout      => m_LayoutEvent ??= new(),
        EventType.MouseDown   => ParseMouseDownEvent(source),
        EventType.MouseUp     => ParseMouseUpEvent(source),
        EventType.MouseDrag   => new OgUnityMouseMoveEvent(),
        EventType.ScrollWheel => ParseMouseScrollEvent(source),
        EventType.KeyDown     => ParseKeyDownEvent(source),
        EventType.KeyUp       => ParseKeyUpEvent(source),
        _                     => throw new InvalidOperationException()
    };
    // Legacy Code.
    /*
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
    */
}
public abstract class OgUnityEventPipe<TEvent> : IOgEventPipe<UnityEngine.Event> where TEvent : class, IOgEvent
{
    private            TEvent?  m_Event;
    public abstract    bool     CanHandle(UnityEngine.Event                  value);
    public             IOgEvent GetEventFromSource(UnityEngine.Event         source) => InternalGetEventFromSource(source);
    protected virtual  TEvent   InternalGetEventFromSource(UnityEngine.Event source) => m_Event ??= Create();
    protected abstract TEvent   Create();
}
public abstract class OgUnityFillEventPipe<TEvent> : OgUnityEventPipe<TEvent> where TEvent : class, IOgEvent
{
    protected override TEvent InternalGetEventFromSource(UnityEngine.Event source) => FillBySource(base.InternalGetEventFromSource(source), source);
    protected abstract TEvent FillBySource(TEvent                          target, UnityEngine.Event source);
}