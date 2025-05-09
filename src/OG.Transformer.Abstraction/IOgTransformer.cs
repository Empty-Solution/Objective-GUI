using UnityEngine;
namespace OG.Transformer.Abstraction;
public interface IOgTransformer<in TOption> : IOgTransformer where TOption : IOgTransformerOption
{
    Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        TOption option);
}
public interface IOgTransformer
{
    int Order { get; }
    Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        IOgTransformerOption option);
}