using DK.Matching;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using System;
using System.Collections.Generic;
namespace OG.Event;
public class OgEventHandlerProvider : IOgEventHandlerProvider
{
    private readonly DkTypeCacheMatcherProvider<Type, IOgEventHandler> m_DkMatchProvider;
    private readonly List<IOgEventHandler>                             m_Handlers;
    public OgEventHandlerProvider()
    {
        m_Handlers        = [];
        m_DkMatchProvider = new(m_Handlers);
    }
    public void Register(IOgEventHandler handler) => m_Handlers.Add(handler);
    public void Unregister(IOgEventHandler handler) => m_Handlers.Remove(handler);
    public bool Handler(IOgEvent reason) => m_DkMatchProvider.TryGetMatcher(reason.GetType(), out IOgEventHandler match) && match.HandleEvent(reason);
}