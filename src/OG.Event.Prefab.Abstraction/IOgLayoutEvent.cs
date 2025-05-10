using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgLayoutEvent : IOgEvent
{
    IEnumerable<IOgTransformer> Transformers         { get; }
    Rect                        ParentRect           { get; set; }
    Rect                        LastLayoutRect       { get; set; }
    public int                  RemainingLayoutItems { get; set; }
}