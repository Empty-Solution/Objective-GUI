using DK.Getting.Generic;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgAnimatedColorTextureBuildContext(OgTextureElement element, OgTransformerRectGetter rectGetter,
    OgAnimationColorGetter<DkReadOnlyGetter<Color>> colorGetter)
    : OgBaseElementBuildContext<OgTextureElement, OgTransformerRectGetter>(element, rectGetter)
{
    public OgAnimationColorGetter<DkReadOnlyGetter<Color>> ColorGetter { get; } = colorGetter;
}