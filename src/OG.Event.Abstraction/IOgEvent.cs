namespace OG.Event.Abstraction;
public interface IOgEvent
{
    // TODO: Remove Useless Methods.
    bool IsConsumed { get; }
    void Consume();
}