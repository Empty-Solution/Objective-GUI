using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Event.Prefab;
public class OgLayoutEvent(IEnumerable<IOgTransformer> transformers) : OgEvent, IOgLayoutEvent
{
    public IEnumerable<IOgTransformer> Transformers { get; } = transformers;
}