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
public class OgTextStateBuilder(IOgElementFactory<OgTextElement, OgTextFactoryArguments> factory, IDkProcessor<OgTextStateBuildContext> processor)
    : OgTextBuilder<OgAnimationStateRectGetter, OgTextStateBuildContext>(factory, processor)
{
    public override OgAnimationStateRectGetter BuildGetter(OgTransformerRectGetter transformer, OgEventHandlerProvider provider,
        IOgAnimator<Rect> animator) =>
        new(transformer, provider, animator);
    public override OgTextStateBuildContext BuildContext(OgTextElement element, OgAnimationStateRectGetter getter, OgOptionsContainer options) =>
        new(element, getter, options);
}