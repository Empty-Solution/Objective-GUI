using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace OG.Builder.Contexts;
public class OgContainerBuildContext(IOgContainer<IOgElement> element, OgTransformerRectGetter rectGetter)
    : OgBaseElementBuildContext<IOgContainer<IOgElement>, OgTransformerRectGetter>(element, rectGetter);