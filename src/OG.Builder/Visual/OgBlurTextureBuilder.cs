using DK.Processing.Abstraction.Generic;
using OG.Animator;
using OG.Animator.Abstraction;
using OG.Builder.Abstraction;
using OG.Builder.Arguments.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
using UnityEngine;
namespace OG.Builder.Visual;
public abstract class OgBlurTextureBuilder<TGetter, TContext>(IOgElementFactory<OgBlurTextureElement, OgTextureFactoryArguments> factory,
    IDkProcessor<TContext> processor)
    : IOgElementBuilder<OgBlurTextureBuildArguments> where TGetter : OgAnimationGetter<Rect> where TContext : OgBlurTextureBuildContext<TGetter>
{
    public IOgElement Build(OgBlurTextureBuildArguments args)
    {
        OgOptionsContainer      options     = new();
        OgEventHandlerProvider  provider    = new();
        OgTransformerRectGetter transformer = new(provider, options);
        TGetter                 getter      = BuildGetter(transformer, provider, new OgRectAnimator());
        OgTextureFactoryArguments factoryArguments = new(args.Name, getter, args.Value, args.Material, args.Borders)
        {
            EventProvider = provider
        };
        OgBlurTextureElement element = factory.Create(factoryArguments);
        getter.RenderCallback = element;
        processor.Process(BuildContext(element, getter, options));
        return element;
    }
    public abstract TGetter BuildGetter(OgTransformerRectGetter transformer, OgEventHandlerProvider provider, IOgAnimator<Rect> animator);
    public abstract TContext BuildContext(OgBlurTextureElement element, TGetter getter, OgOptionsContainer options);
}