using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationColorGetter(IDkGetProvider<Color> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<Color> animator)
    : OgAnimationGetter<Color>(originalGetter, provider, animator)
{
    protected override Color InternalGet(Color originalValue, Color animationValue) =>
        new(originalValue.r + animationValue.r, originalValue.g + animationValue.g, originalValue.b + animationValue.b,
            originalValue.a + animationValue.a);
}