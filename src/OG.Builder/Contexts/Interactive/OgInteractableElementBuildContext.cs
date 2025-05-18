using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgInteractableElementBuildContext<TElement, TChild, TGetter>(TElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<TElement, TGetter>(element, rectGetter, transformerOptions)
    where TElement : IOgInteractableElement<TChild> where TChild : IOgElement where TGetter : OgTransformerRectGetter;