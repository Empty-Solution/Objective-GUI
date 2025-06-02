using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgVisualFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkGetProvider<Color> colorGetter) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public IDkGetProvider<Color> ColorGetter => colorGetter;
}