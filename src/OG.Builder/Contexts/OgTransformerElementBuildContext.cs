using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts;
public class OgTransformerElementBuildContext<TElement, TGetter>(TElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgBaseElementBuildContext<TElement, TGetter>(element, rectGetter) where TElement : IOgElement where TGetter : IDkGetProvider<Rect>
{
    public IOgOptionsContainer TransformerOptions => transformerOptions;
}