using DK.Processing.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Builder.Interactive;
public abstract class
    OgInteractableBuilder<TFactory, TElement, TFactoryArguments, TArguments, TContext, TGetter, TChild>(TFactory factory, IDkProcessor<TContext>? processor)
    : OgBaseBuilder<TFactory, TElement, TFactoryArguments, TArguments, TContext, TGetter>(factory, processor)
    where TFactory : IOgElementFactory<TElement, TFactoryArguments> where TFactoryArguments : OgElementFactoryArguments
    where TContext : IOgBuildContext<TElement, TGetter> where TArguments : OgElementBuildArguments where TElement : IOgContainer<TChild>
    where TGetter : OgTransformerRectGetter where TChild : IOgElement
{
    protected override void InternalProcessContext(TContext context) => context.RectGetProvider.LayoutCallback = context.Element;
}