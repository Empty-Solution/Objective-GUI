using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
namespace OG.Builder.Contexts.Visual;
public class OgTextBuildContext(OgTextElement element, OgAnimationRectGetter<OgTransformerRectGetter> rectGetter)
    : OgBaseElementBuildContext<OgTextElement, OgAnimationRectGetter<OgTransformerRectGetter>>(element, rectGetter);