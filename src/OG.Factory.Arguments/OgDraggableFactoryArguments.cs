using DK.Getting.Abstraction.Generic;
using DK.Setting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgDraggableFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkSetProvider<Rect> rectSetProvider,
    IOgEventHandlerProvider? eventProvider) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public IDkSetProvider<Rect> RectSetProvider { get; set; } = rectSetProvider;
}