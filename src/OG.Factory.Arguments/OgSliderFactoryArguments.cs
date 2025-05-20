using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgSliderFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkFieldProvider<float> valueProvider, IDkReadOnlyRange<float> range)
    : OgValueFactoryArguments<float>(name, rectGetProvider, eventProvider, valueProvider)
{
    public IDkReadOnlyRange<float> Range => range;
}