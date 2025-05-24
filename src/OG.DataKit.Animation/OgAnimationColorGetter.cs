using DK.Getting.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationColorGetter(IOgEventHandlerProvider provider) : OgAnimationGetter<DkReadOnlyGetter<Color>, Color>(new(new(0, 0, 0, 0)), provider)
{
    protected override Color CalculateValue(Color currentValue, Color targetValue, float time) =>
        new(Mathf.Lerp(currentValue.r, targetValue.r, time), Mathf.Lerp(currentValue.g, targetValue.g, time),
            Mathf.Lerp(currentValue.b, targetValue.b, time), Mathf.Lerp(currentValue.a, targetValue.a, time));
    protected override Color AddValue(Color originalValue, Color targetModifier) =>
        new(originalValue.r + targetModifier.r, originalValue.g + targetModifier.g, originalValue.b + targetModifier.b,
            originalValue.a + targetModifier.a);
}