using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgBlurTextureTargetBuildContext(OgBlurTextureElement element, OgAnimationTargetRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgBlurTextureBuildContext<OgAnimationTargetRectGetter>(element, rectGetter, transformerOptions);