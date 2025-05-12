using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgMarginTransformer : OgTransformerBase<OgMarginOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(Rect           rect, Rect parentRect, Rect lastRect, int remaining,
                                   OgMarginOption option) => new(rect.x + option.Margin.x, rect.y + option.Margin.y, rect.width, rect.height);
}