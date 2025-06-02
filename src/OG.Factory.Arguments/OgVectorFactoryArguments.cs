using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgVectorFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkFieldProvider<Vector2> valueProvider) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public IDkFieldProvider<Vector2> ValueProvider { get; set; } = valueProvider;
}