using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Element.Abstraction;
public interface IOgElement
{
    IEnumerable<IOgTransformerOption> TransformerOptions { get; }
    string                            Name               { get; }
    bool                              IsActive           { get; }
    Rect                              ElementRect        { get; set; }
    bool ProcessEvent(IOgEvent reason);
    bool TryGetOption(IOgTransformer transformer, out IOgTransformerOption option);
}