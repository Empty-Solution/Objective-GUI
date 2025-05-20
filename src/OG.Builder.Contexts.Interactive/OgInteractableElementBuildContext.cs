using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgInteractableElementBuildContext<TElement, TChild, TGetter>(TElement element, TGetter rectGetter)
    : OgBaseElementBuildContext<TElement, TGetter>(element, rectGetter)
    where TElement : IOgInteractableElement<TChild> where TChild : IOgElement where TGetter : OgTransformerRectGetter;