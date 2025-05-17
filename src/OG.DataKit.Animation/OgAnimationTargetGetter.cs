using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
namespace OG.DataKit.Animation;
public abstract class OgAnimationTargetGetter<TValue>(IDkGetProvider<TValue> originalGetter, IOgEventHandlerProvider provider,
    IOgAnimator<TValue> animator) : OgAnimationGetter<TValue>(originalGetter, provider, animator) where TValue : notnull
{
    public TValue? Target { get; set; }
    protected override TValue GetTarget() => Target!;
}