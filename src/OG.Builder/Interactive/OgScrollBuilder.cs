using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
using UnityEngine;
namespace OG.Builder.Interactive;
public class OgScrollBuilder(IOgElementFactory<IOgVectorValueElement<IOgElement>, OgScrollFactoryArguments> factory,
    IDkProcessor<OgScrollBuildContext> processor) : IOgElementBuilder<OgScrollBuildArguments>
{
    public IOgElement Build(OgScrollBuildArguments args)
    {
        OgOptionsContainer            options    = new();
        OgEventHandlerProvider        provider   = new();
        OgTransformerRectField        field      = new(provider, options);
        DkObservable<Vector2>         observable = new([]);
        DkObservableProperty<Vector2> property   = new(observable, args.InitialValue);
        OgScrollFactoryArguments factoryArguments = new(args.Name, field, property, field)
        {
            EventProvider = provider
        };
        IOgVectorValueElement<IOgElement> element = factory.Create(factoryArguments);
        field.LayoutCallback = element;
        processor.Process(new(element, field, options, property, observable));
        return element;
    }
}