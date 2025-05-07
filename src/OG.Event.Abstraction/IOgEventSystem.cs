using OG.Event.Prefab.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventSystem
{
    IOgEvent GetCurrent();
}