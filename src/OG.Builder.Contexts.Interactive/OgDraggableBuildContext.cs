using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgDraggableBuildContext(IOgDraggableElement<IOgElement> element, OgTransformerRectField rectGetter)
    : OgInteractableElementBuildContext<IOgDraggableElement<IOgElement>, IOgElement, OgTransformerRectField>(element, rectGetter);