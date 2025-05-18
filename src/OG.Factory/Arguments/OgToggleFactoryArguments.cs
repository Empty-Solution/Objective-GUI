using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgToggleFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkFieldProvider<bool> valueProvider)
    : OgValueFactoryArguments<bool>(name, rectGetProvider, valueProvider);