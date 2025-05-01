namespace OG.Event.Abstraction;
public interface IOgEventProvider
{
    void RegisterHandler(IOgEventHandler handler);
    void UnregisterHandler(IOgEventHandler handler);
    bool Invoke(IOgEvent reason);
}