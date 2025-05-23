using DK.Observing.Abstraction.Generic;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhTabObserver(IOgContainer<IOgElement> source, IOgContainer<IOgElement> target, float thumbHeight, OgTransformerRectGetter rectGetter,
    OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter) : IDkObserver<bool>
{
    public void Update(bool state)
    {
        if(state)
            source.Add(target);
        else
            source.Remove(target);
        Rect rect = separatorSelectorGetter.TargetModifier;
        separatorSelectorGetter.SetTime();
        rect.y                                 = rectGetter.Get().y;
        rect.height                            = state ? thumbHeight : 0;
        separatorSelectorGetter.TargetModifier = rect;
    }
    public void Update(object state)
    {
        if(state is bool value) Update(value);
    }
}