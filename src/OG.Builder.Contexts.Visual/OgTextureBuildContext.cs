using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextureBuildContext(OgTextureElement element, OgAnimationRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgTextureElement, OgAnimationRectGetter>(element, rectGetter, transformerOptions);