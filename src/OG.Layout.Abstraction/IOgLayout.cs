using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Layout.Abstraction;
public interface IOgLayout
{
    public Rect ParentRect           { get; set; }
    public int  RemainingLayoutItems { get; set; }
    Rect ProcessLayout(IOgOptionsContainer options);
}