using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgGettablePositionTransformer : OgBaseTransformer<OgGettablePositionTransformerOption>
{
    public override int Order { get; set; } = 0;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgGettablePositionTransformerOption option) =>
        new(option.X?.Get() ?? rect.x, option.Y?.Get() ?? rect.y, rect.width, rect.height);
}