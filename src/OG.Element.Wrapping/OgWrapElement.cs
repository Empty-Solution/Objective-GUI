using OG.Element.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Element.Wrapping;
public class OgWrapElement<TSource>(string name, TSource source) : IOgElement where TSource : IOgElement
{
    public TSource                           Source             => source;
    public Rect                              ElementRect        => Source.ElementRect;
    public IEnumerable<IOgTransformerOption> TransformerOptions => Source.TransformerOptions;
    public string                            Name               => name;
    public bool                              IsActive           => Source.IsActive;
    Rect IOgElement.                         ElementRect        { get; set; }
    public void AddOption(IOgTransformerOption option) => Source.AddOption(option);
    public bool RemoveOption(IOgTransformerOption option) => Source.RemoveOption(option);
    public bool ProcessEvent(IOgEvent reason) => Source.ProcessEvent(reason);
}