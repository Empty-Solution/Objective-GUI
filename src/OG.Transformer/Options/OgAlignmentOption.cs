using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
using UnityEngine;
namespace OG.Transformer.Options;
public class OgAlignmentOption(TextAnchor alignment) : IOgTransformerOption
{
    public TextAnchor Alignment                       { get; set; } = alignment;
    public bool       CanHandle(IOgTransformer value) => value is OgAlignmentTransformer;
}