using UnityEngine;
namespace OG.Transformer.Abstraction;
public interface IOgTransformer<in TOption> : IOgTransformer where TOption : IOgTransformerOption
{
    Rect Transform(Rect rect, Rect parentRect, TOption option);
}
public interface IOgTransformer
{
    Rect Transform(Rect rect, Rect parentRect, IOgTransformerOption option);
}