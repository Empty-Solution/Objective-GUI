using OG.Event.Abstraction;
using OG.EventSystem.Abstraction;
using System.Collections.Generic;
using UeEvent = UnityEngine.Event;
namespace OG.EventSystem;
public class OgUnityEventSystem(IOgEventHandlerProvider rootProvider, IEnumerable<IOgEventPipe<UeEvent>> pipes) : IOgEventSystem<UeEvent>
{
    private readonly List<IOgEventPipe<UeEvent>> m_Pipes = [..pipes];
    public void Register(IOgEventPipe<UeEvent> pipe) => m_Pipes.Add(pipe);
    public void Unregister(IOgEventPipe<UeEvent> pipe) => m_Pipes.Remove(pipe);
    public void Handle()
    {
        foreach(IOgEventPipe<UeEvent> pipe in m_Pipes) rootProvider.Handle(pipe.GetEvent(UeEvent.current));
    }
}