using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts;
public class OgTransformerElementBuildContext<TElement, TGetter>(TElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions) :
    OgBaseElementBuildContext<TElement, TGetter>(element, rectGetter) where TElement : IOgElement where TGetter : OgTransformerRectGetter
{
    public IOgOptionsContainer TransformerOptions => transformerOptions;
}