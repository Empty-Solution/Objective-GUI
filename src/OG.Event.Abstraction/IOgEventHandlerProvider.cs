using OG.Event.Prefab.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventHandlerProvider
{
    void Register(IOgEventHandler handler);
    void Unregister(IOgEventHandler handler);
    bool Handler(IOgEvent reason);
}