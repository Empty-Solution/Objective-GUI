using DK.Getting.Abstraction.Generic;
using DK.Setting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgDraggableFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkSetProvider<Rect> rectSetProvider) : OgElementFactoryArguments(name, rectGetProvider)
{
    public IDkSetProvider<Rect>      RectSetProvider { get; set; } = rectSetProvider;
}