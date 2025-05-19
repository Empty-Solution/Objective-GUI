using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextTargetBuildContext(OgTextElement element, OgAnimationTargetRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTextBuildContext<OgAnimationTargetRectGetter>(element, rectGetter, transformerOptions);