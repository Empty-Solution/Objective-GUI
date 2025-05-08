using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public abstract class OgTransformerBase<TOption> : IOgTransformer<TOption> where TOption : class, IOgTransformerOption
{
    public abstract int  Order { get; }
    public abstract Rect Transform(Rect rect, Rect parentRect, Rect lastRect, TOption option);
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, IOgTransformerOption option) =>
        Transform(rect, parentRect, lastRect, (option as TOption)!);
}