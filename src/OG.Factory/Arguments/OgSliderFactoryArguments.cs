using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgSliderFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkFieldProvider<float> valueProvider)
    : OgValueFactoryArguments<float>(name, rectGetProvider, valueProvider);