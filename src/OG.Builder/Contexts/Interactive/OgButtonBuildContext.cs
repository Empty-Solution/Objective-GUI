using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgButtonBuildContext(IOgInteractableElement<IOgVisualElement> element, OgTransformerRectGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgInteractableElementBuildContext<IOgInteractableElement<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter>(element, rectGetter, transformerOptions);