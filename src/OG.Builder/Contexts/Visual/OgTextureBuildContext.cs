using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgTextureBuildContext<TGetter>(OgTextureElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgTextureElement, TGetter>(element, rectGetter, transformerOptions) where TGetter : OgAnimationGetter<Rect>;