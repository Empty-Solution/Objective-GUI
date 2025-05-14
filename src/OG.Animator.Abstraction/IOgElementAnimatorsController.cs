using OG.Element.Abstraction;
using System.Collections.Generic;
namespace OG.Animator.Abstraction;
public interface IOgElementAnimatorsController : IOgAnimatorController
{
    IEnumerable<IOgElement> Elements { get; }
    void AddElement(IOgElement element);
    void RemoveElement(IOgElement element);
    void Invoke(EOgAnimationState state);
    void Invoke(int state);
}