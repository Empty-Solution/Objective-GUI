using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgGettableSizeTransformer : OgBaseTransformer<OgGettableSizeTransformerOption>
{
    public override int Order { get; set; } = 0;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgGettableSizeTransformerOption option) =>
        new(rect.x, rect.y, option.Width?.Get() ?? rect.width, option.Height?.Get() ?? rect.height);
}