using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using UnityEngine;
namespace OG.Builder.Abstraction;
public interface IOgBuildContext<TElement, TRectProvider> : IOgBuildContext<TElement> where TRectProvider : IDkGetProvider<Rect>
                                                                                      where TElement : IOgElement
{
    TRectProvider RectGetProvider { get; }
}
public interface IOgBuildContext<TElement> where TElement : IOgElement
{
    TElement Element { get; }
}