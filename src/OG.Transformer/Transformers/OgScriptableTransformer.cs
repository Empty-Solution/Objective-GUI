using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgScriptableTransformer(string name, int order, OgScriptableTransformer.TransformHandler handler) : OgTransformerBase<OgScriptableOption>
{
    public delegate Rect TransformHandler(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgScriptableOption option);
    public          string Name  => name;
    public override int    Order => order;
    public override Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgScriptableOption option) =>
        handler.Invoke(rect, parentRect, lastRect, remaining,
                       option);
}