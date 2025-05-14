using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgMaxSizeTransformer : OgTransformerBase<OgMaxSizeOption>
{
    public override int Order { get; set; } = 999;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgMaxSizeOption option) =>
        new(rect.x, rect.y, Mathf.Min(rect.width, option.MaxWidth), Mathf.Min(rect.height, option.MaxHeight));
}