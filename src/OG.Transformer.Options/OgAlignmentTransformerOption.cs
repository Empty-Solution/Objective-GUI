using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Options;
public class OgAlignmentTransformerOption(TextAnchor alignment = TextAnchor.UpperLeft) : IOgTransformerOption
{
    public TextAnchor Alignment { get; } = alignment;
}