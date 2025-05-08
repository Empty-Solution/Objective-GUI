using OG.Element.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Element.Wrapping;
public class OgWrapElement<TSource>(string name, TSource source) : IOgElement where TSource : IOgElement
{
    public TSource                           Source                        => source;
    public IEnumerable<IOgTransformerOption> TransformerOptions            => Source.TransformerOptions;
    public string                            Name                          => name;
    public bool                              IsActive                      => Source.IsActive;
    public Rect                              ElementRect                   => Source.ElementRect;
    public bool                              ProcessEvent(IOgEvent reason) => Source.ProcessEvent(reason);
    public void ProcessTransformers(IEnumerable<IOgTransformer> transformers, Rect parentRect, Rect lastRect) =>
        Source.ProcessTransformers(transformers, parentRect, lastRect);
}