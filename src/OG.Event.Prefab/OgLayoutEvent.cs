using OG.Event.Prefab.Abstraction;
using OG.Layout.Abstraction;
namespace OG.Event.Prefab;
public class OgLayoutEvent(IOgLayout layout) : OgEvent, IOgLayoutEvent
{
    public IOgLayout Layout { get; } = layout;
}