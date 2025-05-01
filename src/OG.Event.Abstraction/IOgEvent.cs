namespace OG.Event.Abstraction;
public interface IOgEvent
{
    bool IsConsumed { get; }
    void Consume();
}