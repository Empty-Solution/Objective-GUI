using DK.Common.Matching;
using OG.Event.Abstraction;
using System.Collections.Generic;

namespace OG.Event;

public class OgEventProvider : IOgEventProvider
{
    private readonly List<IOgEventHandler> m_MatchList;
    private readonly DkMatcherProvider<IOgEvent, IOgEventHandler> m_Provider;

    public OgEventProvider()
    {
        m_MatchList = [];
        m_Provider = new(m_MatchList);
    }

    public void RegisterHandler(IOgEventHandler handler) => m_MatchList.Add(handler);

    public void UnregisterHandler(IOgEventHandler handler) => m_MatchList.Add(handler);

    public bool Invoke(IOgEvent reason) => m_Provider.TryGetMatcher(reason, out IOgEventHandler handler) && handler.Handle(reason);
}
