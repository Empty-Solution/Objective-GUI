using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgValueFactoryArguments<TValue>(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkFieldProvider<TValue> valueProvider) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public IDkFieldProvider<TValue> ValueProvider { get; set; } = valueProvider;
}