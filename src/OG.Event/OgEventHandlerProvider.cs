using DK.Matching;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using System.Collections.Generic;
namespace OG.Event;
public class OgEventHandlerProvider : IOgEventHandlerProvider
{
    private readonly DkTypeCacheMatcherProvider<IOgEvent, IOgEventHandler> m_DkMatchProvider;
    private readonly List<IOgEventHandler>                                 m_Handlers;
    public OgEventHandlerProvider()
    {
        m_Handlers        = [];
        m_DkMatchProvider = new(m_Handlers);
    }
    public void ForceRegister(IOgEventHandler handler) => m_Handlers.Insert(0, handler);
    public void Register(IOgEventHandler handler) => m_Handlers.Add(handler);
    public void Unregister(IOgEventHandler handler) => m_Handlers.Remove(handler);
    public bool Handle(IOgEvent reason) => m_DkMatchProvider.TryGetMatcher(reason, out IOgEventHandler match) && match.HandleEvent(reason);
}