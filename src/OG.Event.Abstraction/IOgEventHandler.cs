using DK.Common.Matching.Abstraction;

namespace OG.Event.Abstraction;

public interface IOgEventHandler<TEvent> : IOgEventHandler
{
    bool Handle(TEvent reason);
}

public interface IOgEventHandler : IDkMatcher<IOgEvent>
{
    bool Handle(IOgEvent reason);
}
