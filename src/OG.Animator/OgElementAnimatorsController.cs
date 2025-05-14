using OG.Animator.Abstraction;
using OG.Element.Abstraction;
using System.Collections.Generic;
namespace OG.Animator;
public class OgElementAnimatorsController : OgAnimatorsController, IOgElementAnimatorsController
{
    protected readonly List<IOgElement>        m_Elements = [];
    public             IEnumerable<IOgElement> Elements => m_Elements;
    public void AddElement(IOgElement element) => m_Elements.Add(element);
    public void RemoveElement(IOgElement element) => m_Elements.Remove(element);
    public void Invoke(EOgAnimationState state) => Invoke((int)state);
    public void Invoke(int state)
    {
        foreach(IOgElement element in m_Elements) Invoke(state, element);
    }
}