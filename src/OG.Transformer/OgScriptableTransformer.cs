using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgScriptableTransformer : OgBaseTransformer<OgScriptableTransformerOption>
{
    public override int Order { get; set; } = 1111;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgScriptableTransformerOption option) =>
        option.Function.Invoke(rect, parentRect, lastRect, remaining);
}