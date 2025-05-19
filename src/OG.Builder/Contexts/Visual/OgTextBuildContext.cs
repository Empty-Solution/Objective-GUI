using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextBuildContext(OgTextElement element, OgAnimationRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgTextElement, OgAnimationRectGetter>(element, rectGetter, transformerOptions);