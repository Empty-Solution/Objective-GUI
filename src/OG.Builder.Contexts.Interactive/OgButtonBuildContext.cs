using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgButtonBuildContext(IOgInteractableElement<IOgVisualElement> element, OgTransformerRectGetter rectGetter)
    : OgInteractableElementBuildContext<IOgInteractableElement<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter>(element, rectGetter);