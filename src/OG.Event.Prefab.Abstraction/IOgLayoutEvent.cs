using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Event.Prefab.Abstraction;
public interface IOgLayoutEvent : IOgEvent
{
    IEnumerable<IOgTransformer> Transformers { get; }
}