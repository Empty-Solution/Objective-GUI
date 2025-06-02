using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgModalButtonFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider, bool rightClickOnly)
    : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public bool RightClickOnly { get; } = rightClickOnly;
}