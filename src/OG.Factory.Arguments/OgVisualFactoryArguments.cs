using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using OG.DataKit.Animation;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgVisualFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider, Color? color,
    OgAnimationColorGetter<DkReadOnlyGetter<Color>>? colorGetter) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public Color?                                           Color       => color;
    public OgAnimationColorGetter<DkReadOnlyGetter<Color>>? ColorGetter => colorGetter;
}