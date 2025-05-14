using OG.Element.Abstraction;
using System.Collections.Generic;
namespace OG.Animator.Abstraction;
public interface IOgAnimatorController
{
    IEnumerable<IOgAnimator> Animators { get; }
    void AddAnimator(IOgAnimator animator);
    void RemoveAnimator(IOgAnimator animator);
    void Invoke(EOgAnimationState state, IOgElement element);
    void Invoke(int state, IOgElement element);
}