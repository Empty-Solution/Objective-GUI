using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Setting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgScrollFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkFieldProvider<Vector2> valueProvider,
    IDkSetProvider<Rect> rectSetProvider) : OgVectorFactoryArguments(name, rectGetProvider, valueProvider)
{
    public IDkSetProvider<Rect> RectSetProvider { get; set; } = rectSetProvider;
}