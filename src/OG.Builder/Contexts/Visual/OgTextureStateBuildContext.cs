using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextureStateBuildContext(OgTextureElement element, OgAnimationStateRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTextureBuildContext<OgAnimationStateRectGetter>(element, rectGetter, transformerOptions);