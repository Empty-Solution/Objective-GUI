using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgLayoutEvent(IEnumerable<IOgTransformer> transformers) : OgEvent, IOgLayoutEvent
{
    public IEnumerable<IOgTransformer> Transformers         { get; } = transformers;
    public Rect                        ParentRect           { get; set; }
    public Rect                        LastLayoutRect       { get; set; }
    public int                         RemainingLayoutItems { get; set; }
}