using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Abstraction;
public interface IEhWindowBuilder
{
    IOgContainer<IOgElement> Build(out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter, float x = 0, float y = 0);
}