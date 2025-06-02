using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public abstract class OgBaseTransformer<TOption> : IOgTransformer<TOption> where TOption : class, IOgTransformerOption
{
    public abstract int Order { get; set; }
    public abstract Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, TOption option);
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgTransformerOption option) =>
        Transform(rect, parentRect, lastRect, remaining, (option as TOption)!);
    public bool CanHandle(IOgTransformerOption option) => option is TOption;
}