using DK.Getting.Abstraction.Generic;
using DK.Processing.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Builder;
public abstract class
    OgBaseBuilder<TFactory, TElement, TFactoryArguments, TArguments, TContext, TGetter>(TFactory factory, IDkProcessor<TContext>? processor)
    : IOgElementBuilder<TArguments, TElement> where TFactory : IOgElementFactory<TElement, TFactoryArguments>
                                              where TFactoryArguments : OgElementFactoryArguments where TContext : IOgBuildContext<TElement, TGetter>
                                              where TArguments : OgElementBuildArguments where TElement : IOgElement where TGetter : IDkGetProvider<Rect>
{
    public TElement Build(TArguments args)
    {
        OgOptionsContainer     options  = new();
        OgEventHandlerProvider provider = new();
        TContext               context  = BuildContext(args, provider, BuildGetter(args, provider, options));
        TElement               element  = factory.Create(BuildFactoryArguments(context, args, provider));
        context.Element = element;
        InternalProcessContext(context);
        processor?.Process(context);
        return element;
    }
    protected abstract TGetter BuildGetter(TArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container);
    protected abstract TFactoryArguments BuildFactoryArguments(TContext context, TArguments args, IOgEventHandlerProvider provider);
    protected abstract TContext BuildContext(TArguments args, IOgEventHandlerProvider provider, TGetter getter);
    protected abstract void InternalProcessContext(TContext context);
}