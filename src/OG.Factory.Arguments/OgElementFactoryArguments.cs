using DK.Factory.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgElementFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider) : IDkFactoryArguments
{
    public string Name { get; } = name;
    public IDkGetProvider<Rect> RectGetProvider { get; set; } = rectGetProvider;
    public IOgEventHandlerProvider? EventProvider { get; set; } = eventProvider;
}