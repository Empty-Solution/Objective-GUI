using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
using UnityEngine;
namespace OG.Transformer.Options;
public class OgMarginOption(Vector2 margin) : IOgTransformerOption
{
    public Vector2 Margin                          { get; set; } = margin;
    public bool    CanHandle(IOgTransformer value) => value is OgMarginTransformer;
}