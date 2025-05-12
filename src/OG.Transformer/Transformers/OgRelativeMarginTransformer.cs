using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeMarginTransformer : OgTransformerBase<OgRelativeMarginOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(Rect                   rect, Rect parentRect, Rect lastRect, int remaining,
                                   OgRelativeMarginOption option) => new(rect.x * (1 + option.RelativeMarginX), rect.y * (1 + option.RelativeMarginY),
                                                                         rect.width, rect.height);
}