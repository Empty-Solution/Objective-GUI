using OG.Layout.Abstraction;
namespace OG.Event.Prefab.Abstraction;
public interface IOgLayoutEvent : IOgEvent
{
    IOgLayout Layout { get; }
}