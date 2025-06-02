using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgModalButtonBuildContext(IOgModalInteractable<IOgElement> element, OgTransformerRectGetter rectGetter)
    : OgInteractableElementBuildContext<IOgModalInteractable<IOgElement>, IOgElement, OgTransformerRectGetter>(element, rectGetter);