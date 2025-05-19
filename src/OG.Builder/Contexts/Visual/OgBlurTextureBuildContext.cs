using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgBlurTextureBuildContext<TGetter>(OgBlurTextureElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgBlurTextureElement, TGetter>(element, rectGetter, transformerOptions) where TGetter : OgAnimationGetter<Rect>;