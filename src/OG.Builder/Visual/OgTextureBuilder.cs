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
public abstract class OgTextureBuilder<TGetter, TContext>(IOgElementFactory<OgQuadElement, OgTextureFactoryArguments> factory,
    IDkProcessor<TContext> processor)
    : IOgElementBuilder<OgTextureBuildArguments> where TGetter : OgAnimationGetter<Rect> where TContext : OgTextureBuildContext<TGetter>
{
    public IOgElement Build(OgTextureBuildArguments args)
    {
        OgOptionsContainer      options     = new();
        OgEventHandlerProvider  provider    = new();
        OgTransformerRectGetter transformer = new(provider, options);
        TGetter                 getter      = BuildGetter(transformer, provider, new OgRectAnimator()); // таргеты надо в процессоре пихать если что
        OgTextureFactoryArguments factoryArguments = new(args.Name, getter, args.Value, args.Material, args.Borders)
            // я не осилил придумать, откуда пиздить материал, ибо он по идеи должен билдиться при ините чита, так что пусть в чите уже статикой или еще как лежит
            {
                EventProvider = provider
            };
        OgQuadElement element = factory.Create(factoryArguments);
        getter.RenderCallback = element;
        processor.Process(BuildContext(element, getter, options));
        return element;
    }
    public abstract TGetter BuildGetter(OgTransformerRectGetter transformer, OgEventHandlerProvider provider, IOgAnimator<Rect> animator);
    public abstract TContext BuildContext(OgQuadElement element, TGetter getter, OgOptionsContainer options);
}