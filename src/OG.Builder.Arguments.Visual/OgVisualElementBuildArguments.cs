using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgVisualElementBuildArguments(string name, IDkGetProvider<Color> value, IOgEventHandlerProvider? provider) : OgElementBuildArguments(name)
{
    public IDkGetProvider<Color> Value { get; } = value;
    public IOgEventHandlerProvider? Provider { get; } = provider;
}