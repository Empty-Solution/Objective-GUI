using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Visual;
public class OgTextureBuildContext(OgQuadElement element, OgTransformerRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgQuadElement, OgTransformerRectGetter>(element, rectGetter, transformerOptions);