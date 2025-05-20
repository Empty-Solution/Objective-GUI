using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationColorGetter<TGetter>(TGetter originalGetter, IOgEventHandlerProvider provider)
    : OgAnimationGetter<TGetter, Color>(originalGetter, provider) where TGetter : IDkGetProvider<Color>
{
    protected override Color CalculateValue(Color currentValue, Color targetValue, float time) =>
        new(Mathf.Lerp(currentValue.r, targetValue.r, time), Mathf.Lerp(currentValue.g, targetValue.g, time),
            Mathf.Lerp(currentValue.b, targetValue.b, time), Mathf.Lerp(currentValue.a, targetValue.a, time));
    protected override Color CalculateValue(Color originalValue, Color targetModifier) =>
        new(originalValue.r + targetModifier.r, originalValue.g + targetModifier.g, originalValue.b + targetModifier.b,
            originalValue.a + targetModifier.a);
}