using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgMinSizeTransformer : OgBaseTransformer<OgMinSizeTransformerOption>
{
    public override int Order { get; set; } = 999;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgMinSizeTransformerOption option) =>
        new(rect.x, rect.y, Mathf.Max(rect.width, option.MinWidth), Mathf.Max(rect.height, option.MinHeight));
}