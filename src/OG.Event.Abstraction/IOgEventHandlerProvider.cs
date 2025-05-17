using OG.Event.Prefab.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventHandlerProvider
{
    void ForceRegister(IOgEventHandler handler);
    void Register(IOgEventHandler handler);
    void Unregister(IOgEventHandler handler);
    bool Handle(IOgEvent reason);
}