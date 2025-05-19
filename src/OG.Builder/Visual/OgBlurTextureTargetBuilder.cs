using DK.Processing.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
using UnityEngine;
namespace OG.Builder.Visual;
public class OgBlurTextureTargetBuilder(IOgElementFactory<OgBlurTextureElement, OgTextureFactoryArguments> factory,
    IDkProcessor<OgBlurTextureTargetBuildContext> processor)
    : OgBlurTextureBuilder<OgAnimationTargetRectGetter, OgBlurTextureTargetBuildContext>(factory, processor)
{
    public override OgAnimationTargetRectGetter BuildGetter(OgTransformerRectGetter transformer, OgEventHandlerProvider provider,
        IOgAnimator<Rect> animator) =>
        new(transformer, provider, animator);
    public override OgBlurTextureTargetBuildContext BuildContext(OgBlurTextureElement element, OgAnimationTargetRectGetter getter,
        OgOptionsContainer options) =>
        new(element, getter, options);
}