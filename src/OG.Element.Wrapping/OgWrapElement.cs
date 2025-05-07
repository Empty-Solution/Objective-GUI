using OG.Element.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Element.Wrapping;
public class OgWrapElement<TSource>(string name, TSource source) : IOgElement where TSource : IOgElement
{
    public TSource Source                        => source;
    public string  Name                          => name;
    public bool    ProcessEvent(IOgEvent reason) => source.ProcessEvent(reason);
}