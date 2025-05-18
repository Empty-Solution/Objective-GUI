using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
using UnityEngine;
namespace OG.Builder.Interactive;
public class OgVectorBuilder(IOgElementFactory<IOgVectorValueElement<IOgVisualElement>, OgVectorFactoryArguments> factory,
    IDkProcessor<OgVectorBuildContext> processor) : IOgElementBuilder<OgVectorBuildArguments>
{
    public IOgElement Build(OgVectorBuildArguments args)
    {
        OgOptionsContainer            options    = new();
        OgEventHandlerProvider        provider   = new();
        OgTransformerRectGetter       getter     = new(provider, options);
        DkObservable<Vector2>         observable = new([]);
        DkObservableProperty<Vector2> property   = new(observable, args.InitialValue);
        OgVectorFactoryArguments factoryArguments = new(args.Name, getter, property)
        {
            EventProvider = provider
        };
        IOgVectorValueElement<IOgVisualElement> element = factory.Create(factoryArguments);
        getter.LayoutCallback = element;
        processor.Process(new(element, getter, options, property, observable));
        return element;
    }
}