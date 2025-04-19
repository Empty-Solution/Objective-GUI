using DK.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element;

public abstract class OgLayout<TElement> : IOgLayout<TElement> where TElement : IOgElement
{
    protected Rect m_LastRect = Rect.zero;
    public void Dispose() => State = EDkScopeState.Disposed;

    public virtual void Open()
    {
        m_LastRect = Rect.zero;
        State = EDkScopeState.Opened;
    }

    public virtual void Close()
    {
        m_LastRect = Rect.zero;
        State = EDkScopeState.Closed;
    }

    public abstract void ProcessItem(TElement element);
    public EDkScopeState State { get; private set; } = EDkScopeState.Created;
}