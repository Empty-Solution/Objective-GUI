using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgBlurTextureStateBuildContext(OgBlurTextureElement element, OgAnimationStateRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgBlurTextureBuildContext<OgAnimationStateRectGetter>(element, rectGetter, transformerOptions);