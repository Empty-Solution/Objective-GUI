using DK.Getting.Generic;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgAnimatedColorTextBuildContext(OgTextElement element, OgTransformerRectGetter rectGetter,
    OgAnimationColorGetter<DkReadOnlyGetter<Color>> colorGetter) : OgBaseElementBuildContext<OgTextElement, OgTransformerRectGetter>(element, rectGetter)
{
    public OgAnimationColorGetter<DkReadOnlyGetter<Color>> ColorGetter => colorGetter;
}