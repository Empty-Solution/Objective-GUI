using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationStateRectGetter(IDkGetProvider<Rect> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<Rect> animator)
    : OgAnimationStateGetter<Rect>(originalGetter, provider, animator)
{
    protected override Rect InternalGet(Rect originalValue, Rect animationValue) =>
        new(originalValue.position + animationValue.position, originalValue.size + animationValue.size);
}
public class OgAnimationStateColorGetter(IDkGetProvider<Color> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<Color> animator)
    : OgAnimationStateGetter<Color>(originalGetter, provider, animator)
{
    protected override Color InternalGet(Color originalValue, Color animationValue) =>
        new(originalValue.r + animationValue.r, originalValue.g + animationValue.g, originalValue.b + animationValue.b, originalValue.a + animationValue.a);
}