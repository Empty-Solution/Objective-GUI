using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgMinSizeTransformer : OgTransformerBase<OgMinSizeOption>
{
    public override int Order => 999;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, OgMinSizeOption option) =>
        new(rect.x, rect.y, Mathf.Max(rect.width, option.MinWidth), Mathf.Max(rect.height, option.MinHeight));
}