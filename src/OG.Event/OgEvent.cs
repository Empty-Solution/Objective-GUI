using OG.Event.Abstraction;

namespace OG.Event;

public class OgEvent : IOgEvent
{
    public bool IsConsumed { get; private set; }

    public virtual void Consume() => IsConsumed = true;
}
