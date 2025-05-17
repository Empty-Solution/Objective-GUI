using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
using System;
using System.Collections.Generic;
namespace OG.DataKit.Animation;
public abstract class OgAnimationStateGetter<TValue>(IDkGetProvider<TValue> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<TValue> animator)
    : OgAnimationGetter<TValue>(originalGetter, provider, animator) where TValue : notnull
{
    private readonly Dictionary<EOgAnimationState, TValue> m_Targets = [];
    protected        TValue?                               m_Target;
    protected override TValue GetTarget() => m_Target!;
    public void ChangeState(EOgAnimationState state)
    {
        if(!m_Targets.TryGetValue(state, out TValue target)) throw new InvalidOperationException();
        m_Target = target;
    }
    public void SetTarget(EOgAnimationState state, TValue target) => m_Targets[state] = target;
    public void RemoveTarget(EOgAnimationState state) => m_Targets.Remove(state);
}