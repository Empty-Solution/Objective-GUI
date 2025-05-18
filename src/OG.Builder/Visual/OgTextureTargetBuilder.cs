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
public class OgTextureTargetBuilder(IOgElementFactory<OgQuadElement, OgTextureFactoryArguments> factory,
    IDkProcessor<OgTextureTargetBuildContext> processor) : OgTextureBuilder<OgAnimationTargetRectGetter, OgTextureTargetBuildContext>(factory, processor)
{
    public override OgAnimationTargetRectGetter BuildGetter(OgTransformerRectGetter transformer, OgEventHandlerProvider provider,
        IOgAnimator<Rect> animator) =>
        new(transformer, provider, animator);
    public override OgTextureTargetBuildContext BuildContext(OgQuadElement element, OgAnimationTargetRectGetter getter, OgOptionsContainer options) =>
        new(element, getter, options);
}