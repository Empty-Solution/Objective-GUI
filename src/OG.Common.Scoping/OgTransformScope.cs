using DK.Scoping;
using DK.Scoping.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using System;

namespace OG.Common.Scoping;

public abstract class OgTransformScope : DkScope, IOgTransformScope
{
    private IOgTransform? m_Focus;

    public void Focus(IOgTransform transform)
    {
        switch(State)
        {
            case EDkScopeState.Opened: throw new InvalidOperationException(State.ToString());
            case EDkScopeState.Disposed: throw new InvalidOperationException(State.ToString());
            case EDkScopeState.Closed or EDkScopeState.Created:
            m_Focus = transform;
            break;
        }
    }

    protected sealed override void OnClosed()
    {
        OnClosed(m_Focus!);
        m_Focus = null;
    }

    protected sealed override void OnOpened()
    {
        if(m_Focus is null) throw new InvalidOperationException();
        OnOpened(m_Focus);
    }

    protected abstract void OnOpened(IOgTransform focus);

    protected abstract void OnClosed(IOgTransform focus);
}