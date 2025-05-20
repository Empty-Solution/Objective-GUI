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
    OgBaseBuilder<TFactory, TElement, TFactoryArguments, TArguments, TContext, TGetter>(TFactory factory, IDkProcessor<TContext> processor)
    : IOgElementBuilder<TArguments> where TFactory : IOgElementFactory<TElement, TFactoryArguments> where TFactoryArguments : OgElementFactoryArguments
                                    where TContext : IOgBuildContext<TElement, TGetter> where TArguments : OgElementBuildArguments
                                    where TElement : IOgElement where TGetter : IDkGetProvider<Rect>
{
    public IOgElement Build(TArguments args)
    {
        OgOptionsContainer     options          = new();
        OgEventHandlerProvider provider         = new();
        TGetter                getter           = BuildGetter(args, provider, options);
        TContext               context          = BuildContext(args, options, provider, getter);
        TFactoryArguments      factoryArguments = BuildFactoryArguments(context, args, provider);
        TElement               element          = factory.Create(factoryArguments);
        context.Element = element;
        InternalProcessContext(context);
        processor.Process(context);
        return element;
    }
    protected abstract TGetter BuildGetter(TArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container);
    protected abstract TFactoryArguments BuildFactoryArguments(TContext context, TArguments args, IOgEventHandlerProvider provider);
    protected abstract TContext BuildContext(TArguments args, IOgOptionsContainer container, IOgEventHandlerProvider provider, TGetter getter);
    protected abstract void InternalProcessContext(TContext context);
}