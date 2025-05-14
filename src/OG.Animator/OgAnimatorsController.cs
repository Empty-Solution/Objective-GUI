using OG.Animator.Abstraction;
using OG.Element.Abstraction;
using System;
using System.Collections.Generic;
namespace OG.Animator;
public class OgAnimatorsController : IOgAnimatorController
{
    protected readonly List<IOgAnimator>                    m_Animators      = [];
    protected readonly Dictionary<(Type, int), IOgAnimator> m_AnimatorsCache = new();
    public             IEnumerable<IOgAnimator>             Animators => m_Animators;
    public void AddAnimator(IOgAnimator animator) => m_Animators.Add(animator);
    public void RemoveAnimator(IOgAnimator animator) => m_Animators.Remove(animator);
    public void Invoke(EOgAnimationState state, IOgElement element) => Invoke((int)state, element);
    public void Invoke(int state, IOgElement element)
    {
        Type type = element.GetType();
        if(m_AnimatorsCache.TryGetValue((type, state), out IOgAnimator cachedAnimator)) cachedAnimator.Animate(element);

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach(IOgAnimator animator in m_Animators)
        {
            if(!animator.CanHandle(type) || !animator.CanHandle(state)) continue;
            m_AnimatorsCache.Add((type, state), animator);
            animator.Animate(element);
            break;
        }
    }
}