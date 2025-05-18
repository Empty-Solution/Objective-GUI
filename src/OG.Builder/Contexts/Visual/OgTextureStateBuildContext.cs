using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextureStateBuildContext(OgQuadElement element, OgAnimationStateRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTextureBuildContext<OgAnimationStateRectGetter>(element, rectGetter, transformerOptions);