using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgMarginTransformer : OgBaseTransformer<OgMarginTransformerOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgMarginTransformerOption option) =>
        new(rect.x + option.MarginX, rect.y + option.MarginY, rect.width, rect.height);
}