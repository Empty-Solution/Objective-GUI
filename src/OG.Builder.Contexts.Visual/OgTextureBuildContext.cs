using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextureBuildContext(OgTextureElement element, OgAnimationRectGetter<OgTransformerRectGetter> rectGetter)
    : OgBaseElementBuildContext<OgTextureElement, OgAnimationRectGetter<OgTransformerRectGetter>>(element, rectGetter);