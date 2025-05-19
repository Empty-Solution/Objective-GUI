using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextStateBuildContext(OgTextElement element, OgAnimationStateRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTextBuildContext<OgAnimationStateRectGetter>(element, rectGetter, transformerOptions);