using DK.Matching.Abstraction;
using OG.Element.Abstraction;
using System;
namespace OG.Animator.Abstraction;
public interface IOgAnimator<TElement> : IOgAnimator where TElement : IOgElement
{
    void Animate(TElement element);
}
public interface IOgAnimator : IDkMatcher<Type>, IDkMatcher<int>
{
    void Animate(IOgElement element);
}