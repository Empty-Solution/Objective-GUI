using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextureTargetBuildContext(OgTextureElement element, OgAnimationTargetRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTextureBuildContext<OgAnimationTargetRectGetter>(element, rectGetter, transformerOptions);