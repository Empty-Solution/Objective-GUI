using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgMaxSizeTransformer : OgBaseTransformer<OgMaxSizeTransformerOption>
{
    public override int Order { get; set; } = 1020;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgMaxSizeTransformerOption option) =>
        new(rect.x, rect.y, Mathf.Min(rect.width, option.MaxWidth), Mathf.Min(rect.height, option.MaxHeight));
}