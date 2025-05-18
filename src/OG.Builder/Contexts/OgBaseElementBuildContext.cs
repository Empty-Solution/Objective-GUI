using DK.Getting.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts;
public class OgBaseElementBuildContext<TElement, TGetter>(TElement element, TGetter rectGetter)
    : IOgBuildContext<TElement, TGetter> where TElement : IOgElement where TGetter : IDkGetProvider<Rect>
{
    public TElement Element         => element;
    public TGetter  RectGetProvider => rectGetter;
}