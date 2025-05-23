using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Visual.Abstraction;
using UnityEngine;
namespace EH.Builder.Abstraction;
public interface IEhTabButtonBuilder
{
    IOgContainer<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> buildedTabContainer, float x = 0, float y = 0);
}