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
    Rect                              ElementRect        { get; }
    bool                              ProcessEvent(IOgEvent                           reason);
    void                              ProcessTransformers(IEnumerable<IOgTransformer> transformers, Rect parentRect, Rect lastRect);
}