using UnityEngine;
namespace OG.Transformer.Abstraction;
public interface IOgTransformer
{
    int Order { get; }
    Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options);
}