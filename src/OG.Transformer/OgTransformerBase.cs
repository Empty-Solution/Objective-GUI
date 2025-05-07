using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public abstract class OgTransformerBase<TOption> : IOgTransformer<TOption> where TOption : class, IOgTransformerOption
{
    public abstract Rect Transform(Rect rect, Rect parentRect, TOption option);
    public Rect Transform(Rect rect, Rect parentRect, IOgTransformerOption option) => Transform(rect, parentRect, (option as TOption)!);
}