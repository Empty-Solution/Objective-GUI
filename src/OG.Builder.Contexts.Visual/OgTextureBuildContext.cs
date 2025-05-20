using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
namespace OG.Builder.Contexts.Visual;
public class OgTextureBuildContext(OgTextureElement element, OgAnimationRectGetter<OgTransformerRectGetter> rectGetter)
    : OgBaseElementBuildContext<OgTextureElement, OgAnimationRectGetter<OgTransformerRectGetter>>(element, rectGetter);