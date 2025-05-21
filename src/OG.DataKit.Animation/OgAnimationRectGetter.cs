using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationRectGetter<TGetter>(TGetter originalGetter, IOgEventHandlerProvider provider)
    : OgAnimationGetter<TGetter, Rect>(originalGetter, provider) where TGetter : IDkGetProvider<Rect>
{
    protected override Rect CalculateValue(Rect currentValue, Rect targetValue, float time) =>
        new(Mathf.Lerp(currentValue.x, targetValue.x, time), Mathf.Lerp(currentValue.y, targetValue.y, time),
            Mathf.Lerp(currentValue.width, targetValue.width, time), Mathf.Lerp(currentValue.height, targetValue.height, time));
    protected override Rect CalculateValue(Rect originalValue, Rect targetModifier) =>
        new(originalValue.position + targetModifier.position, originalValue.size + targetModifier.size);
}