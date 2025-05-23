using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
namespace OG.Builder.Contexts.Visual;
public class OgQuadBuildContext(OgQuadElement element, OgAnimationRectGetter<OgTransformerRectGetter> rectGetter)
    : OgBaseElementBuildContext<OgQuadElement, OgAnimationRectGetter<OgTransformerRectGetter>>(element, rectGetter);