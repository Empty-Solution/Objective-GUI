using OG.Event.Prefab.Abstraction;
namespace OG.Element.Abstraction;
public interface IOgElement
{
    string Name     { get; }
    bool   IsActive { get; }
    bool ProcessEvent(IOgEvent reason);
}