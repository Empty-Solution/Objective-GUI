using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgVectorFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkFieldProvider<Vector2> valueProvider)
    : OgElementFactoryArguments(name, rectGetProvider)
{
    public IDkFieldProvider<Vector2> ValueProvider { get; set; } = valueProvider;
}