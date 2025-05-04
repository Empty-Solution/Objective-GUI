using DK.Matching;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction;
using System;
using System.Collections.Generic;
using UeEvent = UnityEngine.Event;
namespace OG.Unity.Event;
public class OgUnityEventSystem(IEnumerable<IOgEventPipe<UeEvent>> pipes) : IOgEventSystem
{
    private readonly DkMatcherProvider<UeEvent, IOgEventPipe<UeEvent>> m_MatchProvider = new(pipes);
    public IOgEvent GetCurrent()
    {
        UeEvent source = UeEvent.current;
        if(!m_MatchProvider.TryGetMatcher(source, out IOgEventPipe<UeEvent> pipe)) throw new NullReferenceException();
        return pipe.GetEventFromSource(source);
    }
}