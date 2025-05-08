using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgScriptableTransformer(string name, OgScriptableTransformer.TransformHandler handler) : OgTransformerBase<OgScriptableOption>
{
    public string Name => name;
    public delegate Rect TransformHandler(Rect rect, Rect parentRect, Rect lastRect, OgScriptableOption option);
    public override int Order => 1;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, OgScriptableOption option) => handler.Invoke(rect, parentRect, lastRect, option);
}