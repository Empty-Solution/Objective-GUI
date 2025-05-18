using DK.Getting.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts;
public class OgElementBuildContext<TElement, TChild, TGetter>(TElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions)
    : IOgBuildContext<TElement, TGetter> where TElement : IOgInteractableElement<TChild> where TChild : IOgElement where TGetter : IDkGetProvider<Rect>
{
    public IOgOptionsContainer TransformerOptions => transformerOptions;
    public TElement            Element            => element;
    public TGetter             RectGetProvider    => rectGetter;
}