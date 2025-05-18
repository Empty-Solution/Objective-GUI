using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgValueFactoryArguments<TValue>(string name, IDkGetProvider<Rect> rectGetProvider, IDkFieldProvider<TValue> valueProvider)
    : OgElementFactoryArguments(name, rectGetProvider)
{
    public IDkFieldProvider<TValue> ValueProvider { get; set; } = valueProvider;
}