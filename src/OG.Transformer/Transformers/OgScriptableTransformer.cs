using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgScriptableTransformer(string name, int order, OgScriptableTransformer.TransformHandler handler) : IOgTransformer
{
    public delegate Rect TransformHandler(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options);
    public string Name  => name;
    public int    Order { get; set; } = order;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        handler.Invoke(rect, parentRect, lastRect, remaining, options);
}