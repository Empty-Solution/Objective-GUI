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
public abstract class OgTextBuilder<TGetter, TContext>(IOgElementFactory<OgTextElement, OgTextFactoryArguments> factory, IDkProcessor<TContext> processor)
    : IOgElementBuilder<OgTextBuildArguments> where TGetter : OgAnimationGetter<Rect> where TContext : OgTextBuildContext<TGetter>
{
    public IOgElement Build(OgTextBuildArguments args)
    {
        OgOptionsContainer      options     = new();
        OgEventHandlerProvider  provider    = new();
        OgTransformerRectGetter transformer = new(provider, options);
        TGetter                 getter      = BuildGetter(transformer, provider, new OgRectAnimator());
        OgTextFactoryArguments factoryArguments = new(args.Name, getter, args.Value, args.Font, args.FontSize, args.FontStyle,
                                                      args.PixelsPerUnit, 1f)
        {
            EventProvider = provider
        };
        OgTextElement element = factory.Create(factoryArguments);
        getter.RenderCallback = element;
        processor.Process(BuildContext(element, getter, options));
        return element;
    }
    public abstract TGetter BuildGetter(OgTransformerRectGetter transformer, OgEventHandlerProvider provider, IOgAnimator<Rect> animator);
    public abstract TContext BuildContext(OgTextElement element, TGetter getter, OgOptionsContainer options);
}