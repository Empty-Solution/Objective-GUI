using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationColorGetter<TGetter>(TGetter originalGetter, IOgEventHandlerProvider provider)
    : OgAnimationGetter<TGetter, Color>(originalGetter, provider) where TGetter : IDkGetProvider<Color>
{
    protected override Color InternalGet(Color originalValue, Color targetModifier, float time) =>
        new(originalValue.r + targetModifier.r * time, originalValue.g + targetModifier.g * time, originalValue.b + targetModifier.b * time,
            originalValue.a + targetModifier.a * time);
}