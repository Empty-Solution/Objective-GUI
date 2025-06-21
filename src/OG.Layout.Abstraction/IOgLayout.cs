using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Layout.Abstraction;
public interface IOgLayout
{
    Rect ParentRect { get; set; }
    int RemainingLayoutItems { get; set; }
    Rect LastLayoutRect { get; set; }
    Rect ProcessLayout(Rect rect, IOgOptionsContainer container);
}